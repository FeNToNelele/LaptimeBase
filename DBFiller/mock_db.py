"""
Generates dummy data into LaptimeBase's database.
"""

import sqlite3
from datetime import datetime, timedelta
import random

conn = sqlite3.connect("../LaptimeBaseAPI/LaptimeBaseAPI/LapTimeManager.db")
cursor = conn.cursor()

cars = [(car_class,) for car_class in ['GT3', 'GT4', 'TCX']]
cursor.executemany("INSERT INTO car (class) VALUES (?);", cars)

tracks = [(track_name,) for track_name in ['Hungaroring', 'Nurburgring']]
cursor.executemany("INSERT INTO track (name) VALUES (?);", tracks)

# Insert data into `user`
# users = [(None, f"user{i}", f"pass{i}", int(i % 2 == 0)) for i in range(1, 6)]
# insert_many("INSERT INTO user (id, username, password, is_admin) VALUES (?, ?, ?, ?);", users)

# Insert a sample admin user (username: admin, password: admin as hash)
# Insert a sample non-admin user (username: boss, password: boss as hash)
# AQAAAAEAACcQAAAAEOxFNQ0K7J6+0azkMQDXWHtrhnoFsk444McfCUxvEMTtosg6JSg0on2iF0xQ2EaXDQ==
# users = [(None, "admin", "AQAAAAEAACcQAAAAEOxFNQ0K7J6+0azkMQDXWHtrhnoFsk444McfCUxvEMTtosg6JSg0on2iF0xQ2EaXDQ==", "TRUE"),
#          (None, "boss", "AQAAAAEAACcQAAAAELfk3izhFk7E9rHUb+wuXmtR5BOQeakf0G2XXoiqFt6LsERNNg1fAoWkRjrS0LcOnA==", "FALSE")]
# cursor.executemany("INSERT INTO user (id, username, password, is_admin) VALUES (?, ?, ?, ?);", users)

# insert random teams, one per user

cursor.execute("SELECT id FROM car;")
car_class_ids = [row[0] for row in cursor.fetchall()]

team_names = ['Rhoncus', 'Pharetra Mi', 'Suspendisse', 'Potenti', 'Aenean', 'Vestibulum']
teams = [(random.choice(car_class_ids), team_name) for team_name in team_names]

cursor.executemany("INSERT INTO team (CarId, name) VALUES (?, ?);", teams)

# add random training sessions
cursor.execute("SELECT id FROM track;")
track_ids = [row[0] for row in cursor.fetchall()]

sessions = []
for i in range(5):
    held_at = datetime.now() - timedelta(days=i)
    sessions.append((held_at.isoformat(), random.randint(15, 30), random.randint(20, 45), random.choice(track_ids)))

cursor.executemany("INSERT INTO session (held_at, ambient_temp, track_temp, TrackId) VALUES (?, ?, ?, ?);", sessions)


# Insert random laptimes
cursor.execute("SELECT id FROM session;")
session_ids = [row[0] for row in cursor.fetchall()]

cursor.execute("SELECT id FROM team;")
team_ids = [row[0] for row in cursor.fetchall()]

laptimes = []
for team_id in team_ids:
    for sid in random.sample(session_ids, k=2):
        lap_time = f"00:01:{random.randint(42, 46):02}.{random.randint(0, 999):03}"
        created_at = datetime.now().isoformat()
        laptimes.append((lap_time, created_at, team_id, sid))

cursor.executemany("INSERT INTO laptime (time, created_at, TeamId, SessionId) VALUES (?, ?, ?, ?);", laptimes)

# 300 sample laps for Hungaroring, training session id 2

cursor.execute("SELECT id FROM session WHERE TrackId = (SELECT id FROM track WHERE name = 'Hungaroring');")
session_id = cursor.fetchone()[0]

laptimes = []

for _ in range(300):
    for team_id in team_ids:
        lap_time = f"00:01:{random.randint(42, 46):02}.{random.randint(0, 999):03}"
        created_at = datetime.now().isoformat()
        laptimes.append((lap_time, created_at, team_id, session_id))

cursor.executemany("INSERT INTO laptime (time, created_at, TeamId, SessionId) VALUES (?, ?, ?, ?);", laptimes)

conn.commit()
conn.close()
print("Mock data inserted successfully.")