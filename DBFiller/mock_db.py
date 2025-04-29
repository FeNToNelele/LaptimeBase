"""
Generates dummy data into LaptimeBase's database.
"""

import sqlite3
from datetime import datetime, timedelta
import random

conn = sqlite3.connect("../LaptimeBaseAPI/LapTimeManager.db")
cursor = conn.cursor()

cars = [(None, car_class) for car_class in ['GT3', 'GT4', 'TCX']]
cursor.executemany("INSERT INTO car (id, class) VALUES (?, ?);", cars)

tracks = [(None, track_name) for track_name in ['Hungaroring', 'Nurburgring']]
cursor.executemany("INSERT INTO track (id, name) VALUES (?, ?);", tracks)

# Insert data into `user`
# users = [(None, f"user{i}", f"pass{i}", int(i % 2 == 0)) for i in range(1, 6)]
# insert_many("INSERT INTO user (id, username, password, is_admin) VALUES (?, ?, ?, ?);", users)

# Insert a sample admin user (username: admin, password: admin as hash)
# Insert a sample non-admin user (username: boss, password: boss as hash)
# AQAAAAEAACcQAAAAEOxFNQ0K7J6+0azkMQDXWHtrhnoFsk444McfCUxvEMTtosg6JSg0on2iF0xQ2EaXDQ==
users = [(None, "admin", "AQAAAAEAACcQAAAAEOxFNQ0K7J6+0azkMQDXWHtrhnoFsk444McfCUxvEMTtosg6JSg0on2iF0xQ2EaXDQ==", "TRUE"),
         (None, "boss", "AQAAAAEAACcQAAAAELfk3izhFk7E9rHUb+wuXmtR5BOQeakf0G2XXoiqFt6LsERNNg1fAoWkRjrS0LcOnA==", "FALSE")]
cursor.executemany("INSERT INTO user (id, username, password, is_admin) VALUES (?, ?, ?, ?);", users)

# add random training sessions
cursor.execute("SELECT id FROM track;")
track_ids = [row[0] for row in cursor.fetchall()]
sessions = []
for i in range(5):
    held_at = datetime.now() - timedelta(days=i)
    sessions.append((None, held_at.isoformat(), random.randint(15, 30), random.randint(20, 45), random.choice(track_ids)))
cursor.executemany("INSERT INTO session (id, held_at, ambient_temp, track_temp, track_id) VALUES (?, ?, ?, ?, ?);", sessions)

# insert random teams, one per user
cursor.execute("SELECT id FROM user;")
team_owner_ids = [row[0] for row in cursor.fetchall()]
cursor.execute("SELECT id FROM car;")
car_class_ids = [row[0] for row in cursor.fetchall()]
team_names = ['Rhoncus', 'Pharetra Mi', 'Suspendisse', 'Potenti', 'Aenean', 'Vestibulum']
teams = [
    (team_owner_id, random.choice(car_class_ids), f"{random.choice(team_names)}_{team_owner_id}")
    for team_owner_id in team_owner_ids
]
cursor.executemany("INSERT INTO team (user_id, car_id, name) VALUES (?, ?, ?);", teams)

# Insert random laptimes
cursor.execute("SELECT id FROM session;")
session_ids = [row[0] for row in cursor.fetchall()]
laptimes = []
for team_id in team_owner_ids:  # team_id = user_id in team table
    for sid in random.sample(session_ids, k=2):
        lap_time = f"1:{random.randint(42, 46):02}.{random.randint(0, 999):03}"
        created_at = datetime.now().isoformat()
        laptimes.append((None, lap_time, created_at, team_id, sid))
cursor.executemany("INSERT INTO laptime (id, time, created_at, team_id, session_id) VALUES (?, ?, ?, ?, ?);", laptimes)

# 300 sample laps for Hungaroring, training session id 2
laptimes = []

for _ in range(300):
    for team_id in team_owner_ids:  # team_id = user_id in team table
            lap_time = f"1:{random.randint(42,46):02}.{random.randint(0,999):03}"
            created_at = datetime.now().isoformat()
            laptimes.append((None, lap_time, created_at, team_id, 2))

cursor.executemany("INSERT INTO laptime (id, time, created_at, team_id, session_id) VALUES (?, ?, ?, ?, ?);", laptimes)

conn.commit()
conn.close()
print("Mock data inserted successfully.")