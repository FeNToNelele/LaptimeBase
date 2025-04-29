BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
	"MigrationId"	TEXT NOT NULL,
	"ProductVersion"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY("MigrationId")
);
CREATE TABLE IF NOT EXISTS "__EFMigrationsLock" (
	"Id"	INTEGER NOT NULL,
	"Timestamp"	TEXT NOT NULL,
	CONSTRAINT "PK___EFMigrationsLock" PRIMARY KEY("Id")
);
CREATE TABLE IF NOT EXISTS "car" (
	"id"	INTEGER NOT NULL,
	"class"	TEXT NOT NULL,
	CONSTRAINT "PK_car" PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "laptime" (
	"id"	INTEGER NOT NULL,
	"time"	TEXT NOT NULL,
	"created_at"	TEXT NOT NULL,
	"team_id"	INTEGER NOT NULL,
	"session_id"	INTEGER NOT NULL,
	CONSTRAINT "PK_laptime" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_laptime_session_session_id" FOREIGN KEY("session_id") REFERENCES "session"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_laptime_team_team_id" FOREIGN KEY("team_id") REFERENCES "team"("user_id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "session" (
	"id"	INTEGER NOT NULL,
	"held_at"	TEXT NOT NULL,
	"ambient_temp"	INTEGER NOT NULL,
	"track_temp"	INTEGER NOT NULL,
	"track_id"	INTEGER NOT NULL,
	CONSTRAINT "PK_session" PRIMARY KEY("id" AUTOINCREMENT),
	CONSTRAINT "FK_session_track_track_id" FOREIGN KEY("track_id") REFERENCES "track"("id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "team" (
	"user_id"	INTEGER NOT NULL,
	"car_id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	CONSTRAINT "AK_team_user_id" UNIQUE("user_id"),
	CONSTRAINT "PK_team" PRIMARY KEY("user_id","car_id"),
	CONSTRAINT "FK_team_car_car_id" FOREIGN KEY("car_id") REFERENCES "car"("id") ON DELETE CASCADE,
	CONSTRAINT "FK_team_user_user_id" FOREIGN KEY("user_id") REFERENCES "user"("id") ON DELETE CASCADE
);
CREATE TABLE IF NOT EXISTS "track" (
	"id"	INTEGER NOT NULL,
	"name"	TEXT NOT NULL,
	CONSTRAINT "PK_track" PRIMARY KEY("id" AUTOINCREMENT)
);
CREATE TABLE IF NOT EXISTS "user" (
	"id"	INTEGER NOT NULL,
	"username"	TEXT NOT NULL,
	"password"	TEXT NOT NULL,
	"is_admin"	INTEGER NOT NULL,
	CONSTRAINT "PK_user" PRIMARY KEY("id" AUTOINCREMENT)
);
INSERT INTO "__EFMigrationsHistory" VALUES ('20250408090527_InitialCreate','9.0.2');
CREATE INDEX IF NOT EXISTS "IX_laptime_session_id" ON "laptime" (
	"session_id"
);
CREATE INDEX IF NOT EXISTS "IX_laptime_team_id" ON "laptime" (
	"team_id"
);
CREATE INDEX IF NOT EXISTS "IX_session_track_id" ON "session" (
	"track_id"
);
CREATE INDEX IF NOT EXISTS "IX_team_car_id" ON "team" (
	"car_id"
);
COMMIT;
