.PHONY: setup
setup:
	docker-compose build

.PHONY: build
build:
	docker-compose build base-api

.PHONY: serve
serve:
	docker-compose build base-api && docker-compose up base-api

.PHONY: shell
shell:
	docker-compose run base-api bash

.PHONY: test
test:
	docker-compose up test-database & docker-compose build base-api-test && docker-compose up base-api-test

.PHONY: migrate-local-test-database
migrate-test-database:
	-dotnet tool install -g dotnet-ef
	cd ResidentContactApi && CONNECTION_STRING="Host=127.0.0.1;Port=5432;Username=postgres;Password=mypassword;Database=testdb" dotnet ef database update

.PHONY: lint
lint:
	-dotnet tool install -g dotnet-format
	dotnet tool update -g dotnet-format
	dotnet format
