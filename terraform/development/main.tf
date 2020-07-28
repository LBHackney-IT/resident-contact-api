provider "aws" {
  region  = "eu-west-2"
  version = "~> 2.0"
}
data "aws_caller_identity" "current" {}
data "aws_region" "current" {}
locals {
  application_name = "Resident Contact API"
   parameter_store = "arn:aws:ssm:${data.aws_region.current.name}:${data.aws_caller_identity.current.account_id}:parameter"
}

terraform {
  backend "s3" {
    bucket  = "terraform-state-development-apis"
    encrypt = true
    region  = "eu-west-2"
    key     = "services/resident-contact-api/state"
  }
}
data "aws_vpc" "development_vpc" {
    tags = {
        Name = "vpc-development-apis-development"
    }
}
data "aws_subnet_ids" "development" {
    vpc_id = data.aws_vpc.development_vpc.id
    filter {
        name   = "tag:Type"
        values = ["private"]
    }
}
data "aws_ssm_parameter" "resident_contact_postgres_password" {
    name = "/resident-contact-api/development/postgres-password"
}

data "aws_ssm_parameter" "resident_contact_postgres_username" {
    name = "/resident-contact-api/development/postgres-username"
}

module "postgres_db_development" {
    source = "github.com/LBHackney-IT/aws-hackney-common-terraform.git//modules/database/postgres"
    environment_name = "development"
    vpc_id = data.aws_vpc.development_vpc.id
    db_identifier = "resident-contact-database"
    db_name = "resident-contact-database"
    db_port  = 5001
    subnet_ids = data.aws_subnet_ids.development.ids
    db_engine = "postgres"
    db_engine_version = "11.1" //DMS does not work well with v12
    db_instance_class = "db.t2.micro"
    db_allocated_storage = 20
    maintenance_window ="sun:10:00-sun:10:30"
    db_username = data.aws_ssm_parameter.resident_contact_postgres_username.value
    db_password = data.aws_ssm_parameter.resident_contact_postgres_password.value
    storage_encrypted = false
    multi_az = false
    publicly_accessible = false
    project_name = "platform apis"
}
