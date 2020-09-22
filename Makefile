.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build resident-contact-api

.PHONY: serve
serve:
	docker-compose build resident-contact-api && docker-compose up resident-contact-api

.PHONY: shell
shell:
	docker-compose run resident-contact-api bash

.PHONY: test
test:
	docker-compose up test-database & docker-compose build resident-contact-api-test && docker-compose up resident-contact-api-test

.PHONY: migrate-local-test-database
migrate-local-test-database:
	-dotnet tool install -g dotnet-ef
	docker-compose up -d test-database
	CONNECTION_STRING="Host=127.0.0.1;Port=5432;Username=postgres;Password=mypassword;Database=testdb" dotnet ef database update -p ResidentContactApi

.PHONY: restart-db
restart-db:
	docker stop $$(docker ps -q --filter ancestor=test-database -a)
	-docker rm $$(docker ps -q --filter ancestor=test-database -a)
	docker rmi test-database
	docker-compose up -d test-database

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format


