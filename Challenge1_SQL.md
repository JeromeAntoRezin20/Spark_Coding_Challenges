**Crime Management Shema DDL and DML**

```sql
CREATE DATABASE Coding_Challenge

use Coding_Challenge

CREATE TABLE Crime (
    CrimeID INT PRIMARY KEY,
    IncidentType VARCHAR(255),
    IncidentDate DATE,
    Location VARCHAR(255),
    Description TEXT,
    Status VARCHAR(20)
);

CREATE TABLE Victim (
    VictimID INT PRIMARY KEY,
    CrimeID INT,
    Name VARCHAR(255),
    ContactInfo VARCHAR(255),
    Injuries VARCHAR(255),
    Age INT,
    FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID) ON DELETE CASCADE
);

CREATE TABLE Suspect (
    SuspectID INT PRIMARY KEY,
    CrimeID INT,
    Name VARCHAR(255),
    Description TEXT,
    CriminalHistory TEXT,
    Age INT,
    FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID) ON DELETE CASCADE
);


INSERT INTO Crime (CrimeID, IncidentType, IncidentDate, Location, Description, Status) VALUES
(1, 'Robbery', '2023-09-15', '123 Main St, Cityville', 'Armed robbery at convenience store', 'Open'),
(2, 'Homicide', '2023-09-20', '456 Elm St, Townsville', 'Murder investigation', 'Under Investigation'),
(3, 'Theft', '2023-09-10', '789 Oak St, Villagetown', 'Shoplifting incident', 'Closed'),
(4, 'Burglary', '2023-09-05', '321 Pine St, Metropolis', 'Home break-in', 'Open'),
(5, 'Assault', '2023-09-12', '654 Maple Dr, Gotham', 'Bar fight', 'Closed'),
(6, 'Fraud', '2023-09-18', '987 Cedar Ln, Star City', 'Credit card scam', 'Under Investigation'),
(7, 'Vandalism', '2023-09-22', '159 Birch Blvd, Central City', 'Graffiti damage', 'Open'),
(8, 'Kidnapping', '2023-09-08', '753 Spruce Way, Coast City', 'Child abduction', 'Under Investigation'),
(9, 'Arson', '2023-09-25', '852 Willow Ct, National City', 'Warehouse fire', 'Open'),
(10, 'Cybercrime', '2023-09-30', '963 Aspen Ave, Bludhaven', 'Data breach', 'Under Investigation');

INSERT INTO Victim (VictimID, CrimeID, Name, ContactInfo, Injuries, Age) VALUES
(1, 1, 'John Doe', 'johndoe@example.com', 'Minor injuries', 45),
(2, 2, 'Jane Smith', 'janesmith@example.com', 'Deceased', 32),
(3, 3, 'Alice Johnson', 'alicejohnson@example.com', 'None', 28),
(4, 4, 'Bob Williams', 'bob@example.com', 'Bruises', 35),
(5, 5, 'Charlie Brown', 'charlie@example.com', 'Broken nose', 30),
(6, 6, 'Diana Prince', 'diana@example.com', 'None', 27),
(7, 7, 'Edward Norton', 'edward@example.com', 'None', 42),
(8, 8, 'Fiona Green', 'fiona@example.com', 'Trauma', 29),
(9, 9, 'George Miller', 'george@example.com', 'Smoke inhalation', 50),
(10, 10, 'Hannah Baker', 'hannah@example.com', 'None', 31);

INSERT INTO Suspect (SuspectID, CrimeID, Name, Description, CriminalHistory, Age) VALUES
(1, 1, 'Robber 1', 'Armed and masked robber', 'Previous robbery convictions', 30),
(2, 2, 'Unknown', 'Investigation ongoing', NULL, NULL),
(3, 3, 'Suspect 1', 'Shoplifting suspect', 'Prior shoplifting arrests', 22),
(4, 4, 'Burglar X', 'Wears black hoodie', '3 prior burglaries', 35),
(5, 5, 'Fighter Y', 'Tattoo on left arm', 'Multiple assault charges', 28),
(6, 6, 'Scammer Z', 'Well-dressed male', 'Fraud history', 40),
(7, 7, 'Graffiti Artist', 'Teenager with skateboard', 'First offense', 17),
(8, 8, 'Kidnapper A', 'White van driver', 'No prior record', 38),
(9, 9, 'Arsonist B', 'Disgruntled employee', 'Suspected in 2 other fires', 45),
(10, 10, 'Hacker C', 'Computer expert', 'Cybercrime history', 25);

Select *from Crime
Select *from Victim
Select *from Suspect
```
![image](https://github.com/user-attachments/assets/cf992948-81e0-4736-8080-c70eecaf28ca)


--1. Select all open incidents.
```sql
Select *from Crime
where status = 'open'
```
![image](https://github.com/user-attachments/assets/685b6dbf-2e00-465b-86e7-d8f3d0fe6c74)


--2 Find the total number of incidents. 
```sql
Select Count(*) as Total_Incidents from crime
```
![image](https://github.com/user-attachments/assets/685d71b3-587d-4d92-a8e9-3eee7c4c22eb)

--3 List all unique incident types.
```sql
Select Distinct IncidentType from Crime

```
![image](https://github.com/user-attachments/assets/0361b27e-05d7-466e-80ec-ec6a7fed3819)


--4 Retrieve incidents that occurred between '2023-09-01' and '2023-09-10'.
```sql
Select *from Crime
where IncidentDate between '2023-09-01' and '2023-09-10'
```
![image](https://github.com/user-attachments/assets/9b8cea29-256a-4e93-9502-1ccb87145961)

--5 List persons involved in incidents in descending order of age. 
```sql
SELECT Name, Age, 'Victim' AS Role FROM Victim WHERE Age IS NOT NULL
UNION ALL
SELECT Name, Age, 'Suspect' AS Role FROM Suspect WHERE Age IS NOT NULL
ORDER BY Age DESC;
```
![image](https://github.com/user-attachments/assets/217cc49d-85d3-4248-99d8-9d335447dd23)


--6 Find the average age of persons involved in incidents. 
```sql
SELECT AVG(Age) AS AverageAge FROM (
    SELECT Age FROM Victim WHERE Age IS NOT NULL
    UNION ALL
    SELECT Age FROM Suspect WHERE Age IS NOT NULL
) AS AllAges;
```
![image](https://github.com/user-attachments/assets/d86be81a-d2b6-4392-89ca-dd8f428285d3)

--7  List incident types and their counts, only for open cases.
```sql
SELECT IncidentType, COUNT(*) AS IncidentCount
FROM Crime
WHERE Status = 'Open'
GROUP BY IncidentType
```
![image](https://github.com/user-attachments/assets/cf24cd0b-5cde-4032-a72d-d1ad80f46a23)

--8  Find persons with names containing 'Doe'. 
```sql
SELECT Name FROM Victim WHERE Name LIKE '%Doe%'
UNION
SELECT Name FROM Suspect WHERE Name LIKE '%Doe%';
```

![image](https://github.com/user-attachments/assets/23b014d6-2bf3-43d0-a3aa-58ccd3bcd2b3)

--9 Retrieve the names of persons involved in open cases and closed cases.
```sql
SELECT v.Name, c.Status, 'Victim' AS Role
FROM Victim v JOIN Crime c ON v.CrimeID = c.CrimeID
WHERE c.Status IN ('Open', 'Closed')
UNION
SELECT s.Name, c.Status, 'Suspect' AS Role
FROM Suspect s JOIN Crime c ON s.CrimeID = c.CrimeID
WHERE c.Status IN ('Open', 'Closed');
```
![image](https://github.com/user-attachments/assets/234878ec-c30e-481b-a58a-c92894759ba8)



--10  List incident types where there are persons aged 30 or 35 involved. 
```sql
SELECT DISTINCT c.IncidentType
FROM Crime c
WHERE c.CrimeID IN (
    SELECT v.CrimeID FROM Victim v WHERE v.Age IN (30, 35)
    UNION
    SELECT s.CrimeID FROM Suspect s WHERE s.Age IN (30, 35)
);

```

![image](https://github.com/user-attachments/assets/68e9192b-cc3f-42e7-9af0-e521e085ddb0)


--11 Find persons involved in incidents of the same type as 'Robbery'. 
```sql
SELECT v.Name, 'Victim' AS Role
FROM Victim v JOIN Crime c ON v.CrimeID = c.CrimeID
WHERE c.IncidentType = 'Robbery'
UNION
SELECT s.Name, 'Suspect' AS Role
FROM Suspect s JOIN Crime c ON s.CrimeID = c.CrimeID
WHERE c.IncidentType = 'Robbery';

```

![image](https://github.com/user-attachments/assets/f8929108-f891-45d6-ac48-c25a9b866daa)

--12  List incident types with more than one open case. 
```sql
SELECT IncidentType, COUNT(*) AS OpenCaseCount
FROM Crime
WHERE Status = 'Open'
GROUP BY IncidentType
HAVING COUNT(*) > 1;
```

![image](https://github.com/user-attachments/assets/086dfce8-6bc7-4858-a844-533fd70f8510)


--13 List all incidents with suspects whose names also appear as victims in other incidents.
```sql
update Suspect
set name = 'John Doe'
where SuspectID = 1

select c.*
from Crime c
join Suspect s ON c.CrimeID = s.CrimeID
WHERE s.Name IN (select Name from Victim);
```

![image](https://github.com/user-attachments/assets/5043edfe-46fa-4719-830d-a3db4cfacb02)

--14  Retrieve all incidents along with victim and suspect details.
```sql
SELECT 
    c.CrimeID, c.IncidentType, c.IncidentDate, c.Status,
    v.Name AS VictimName, v.Age AS VictimAge, v.Injuries,
    s.Name AS SuspectName, s.Age AS SuspectAge, s.Description AS SuspectDesc
FROM Crime c
LEFT JOIN Victim v ON c.CrimeID = v.CrimeID
LEFT JOIN Suspect s ON c.CrimeID = s.CrimeID;
```

![image](https://github.com/user-attachments/assets/4477a607-30d1-41da-a673-955cbaae3b34)

--15  Find incidents where the suspect is older than any victim.
```sql
SELECT c.*
FROM Crime c
JOIN Suspect s ON c.CrimeID = s.CrimeID
JOIN Victim v ON c.CrimeID = v.CrimeID
WHERE s.Age > v.Age;
```
![image](https://github.com/user-attachments/assets/7fc478b0-7d16-467c-93e9-c3282fe24079)


--16  Find suspects involved in multiple incidents:
```sql
SELECT s.Name, COUNT(*) AS IncidentCount
FROM Suspect s
GROUP BY s.Name
HAVING COUNT(*) > 1;
```

![image](https://github.com/user-attachments/assets/aea12d40-6558-4691-a14a-ff7782b2a5bf)

--17 List incidents with no suspects involved. 
```sql
select c.incidenttype, s.name
from crime c
join Suspect s on c.crimeId = s.CrimeID
where s.name = 'Unknown'
```

![image](https://github.com/user-attachments/assets/a45ef6d1-b779-4e56-9222-49a4126f57de)

--18 List all cases where at least one incident is of type 'Homicide' and all other incidents are of type 
'Robbery'. 


--19 Retrieve a list of all incidents and the associated suspects, showing suspects for each incident, or 
'No Suspect' if there are none.
```sql
Select c.*,s.name
from crime c
join suspect s on c.crimeId = s.crimeID
```

![image](https://github.com/user-attachments/assets/e4c168fc-c343-4aaa-9be5-335b6634521b)


--20  List all suspects who have been involved in incidents with incident types 'Robbery' or 'Assault'
```sql
select s.*, c.incidentType
from crime c
join Suspect s on c.crimeId = s.crimeID
where c.incidentType in ('Robbery','Assualt')
```

![image](https://github.com/user-attachments/assets/ac8d4b04-67d7-4bc3-9e9f-b8424cf26817)

