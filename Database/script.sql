CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    migration_id character varying(150) NOT NULL,
    product_version character varying(32) NOT NULL,
    CONSTRAINT pk___ef_migrations_history PRIMARY KEY (migration_id)
);

START TRANSACTION;

CREATE TABLE locations (
    id integer GENERATED ALWAYS AS IDENTITY,
    street text NOT NULL,
    city text NOT NULL,
    post_code integer NOT NULL,
    state text NOT NULL,
    country text NOT NULL,
    CONSTRAINT pk_locations PRIMARY KEY (id)
);

CREATE TABLE routes (
    id integer GENERATED ALWAYS AS IDENTITY,
    travelling_type text NOT NULL,
    estimated_time_in_seconds double precision NOT NULL,
    distance_in_km double precision NOT NULL,
    map_image bytea NULL,
    CONSTRAINT pk_routes PRIMARY KEY (id)
);

CREATE TABLE tours (
    id integer GENERATED ALWAYS AS IDENTITY,
    location_start_id integer NOT NULL,
    location_destination_id integer NOT NULL,
    route_id integer NOT NULL,
    name text NOT NULL,
    travelling_type text NOT NULL,
    start_date timestamp with time zone NOT NULL,
    popularity integer NOT NULL,
    child_friendliness integer NOT NULL,
    CONSTRAINT pk_tours PRIMARY KEY (id),
    CONSTRAINT fk_tours_locations_location_destination_id FOREIGN KEY (location_destination_id) REFERENCES locations (id) ON DELETE CASCADE,
    CONSTRAINT fk_tours_locations_location_start_id FOREIGN KEY (location_start_id) REFERENCES locations (id) ON DELETE CASCADE,
    CONSTRAINT fk_tours_routes_route_id FOREIGN KEY (route_id) REFERENCES routes (id) ON DELETE CASCADE
);

CREATE TABLE "tourLogs" (
    id integer GENERATED ALWAYS AS IDENTITY,
    date timestamp with time zone NOT NULL,
    comment text NOT NULL,
    difficulty integer NOT NULL,
    taken_time_in_seconds double precision NOT NULL,
    rating integer NOT NULL,
    tour_id integer NOT NULL,
    CONSTRAINT pk_tour_logs PRIMARY KEY (id),
    CONSTRAINT fk_tour_logs_tours_tour_id FOREIGN KEY (tour_id) REFERENCES tours (id) ON DELETE CASCADE
);

CREATE INDEX ix_tour_logs_tour_id ON "tourLogs" (tour_id);

CREATE UNIQUE INDEX ix_tours_location_destination_id ON tours (location_destination_id);

CREATE UNIQUE INDEX ix_tours_location_start_id ON tours (location_start_id);

CREATE UNIQUE INDEX ix_tours_route_id ON tours (route_id);

INSERT INTO "__EFMigrationsHistory" (migration_id, product_version)
VALUES ('20230404173429_InitialCreate', '7.0.3');

COMMIT;

