CREATE TABLE residents (
    id                  integer PRIMARY KEY,
    first_name          varchar(255) NOT NULL,
    last_name           varchar(255),
    date_of_birth       timestamp,
    gender              char(1) NOT NULL
);

CREATE TABLE contact_details (
  id                        integer PRIMARY KEY,
  type_lookup_id            integer NOT NULL,
  subtype_lookup_id         integer NOT NULL,
  contact_details_value     varchar(255) NOT NULL,
  is_default                boolean NOT NULL,
  is_active                 boolean NOT NULL,
  added_by                  varchar(255),
  date_added                timestamp NOT NULL,
  date_modified             timestamp,
  modified_by               varchar(255),
  resident_id               integer REFERENCES residents(id)
);

CREATE TABLE external_system_records(
    resident_id                     integer REFERENCES residents(id),
    external_system_field_value     varchar(255) NOT NULL,
    external_system_field_name      varchar(255) NOT NULL,
    source_system_id                integer NOT NULL
);

CREATE TABLE contact_type_lookup(
    type_lookup_id              integer UNIQUE NOT NULL,
    type_value                  varchar(50) NOT NULL,
    type_description            varchar(100) 
);
CREATE TABLE contact_subtype_lookup(
    subtype_lookup_id           integer UNIQUE NOT NULL,
    subtype_value               varchar(50) NOT NULL,
    subtype_description         varchar(100) 
);

CREATE TABLE source_systems_lookup(
    source_system_lookup_id     integer UNIQUE NOT NULL,
    source_system_value         varchar(50) NOT NULL,
    source_sytem_description    varchar(255) 
);