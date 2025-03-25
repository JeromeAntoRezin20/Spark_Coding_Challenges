**Crime Management Shema DDL and DML**

```sql
-- Create tables 
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
	age int,
    ContactInfo VARCHAR(255), 
    Injuries VARCHAR(255), 
    FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID) ON DELETE CASCADE
); 
 
CREATE TABLE Suspect ( 
    SuspectID INT PRIMARY KEY, 
    CrimeID INT, 
    Name VARCHAR(255), 
    Description TEXT, 
    CriminalHistory TEXT, 
    FOREIGN KEY (CrimeID) REFERENCES Crime(CrimeID) ON DELETE CASCADE
); 
 -- Insert sample data 
INSERT INTO Crime (CrimeID, IncidentType, IncidentDate, Location, Description, Status) VALUES 
    (1, 'Robbery', '2023-09-15', '123 Main St, Cityville', 'Armed robbery at a convenience store', 'Open'), 
    (2, 'Homicide', '2023-09-20', '456 Elm St, Townsville', 'Investigation into a murder case', 'Under Investigation'), 
    (3, 'Theft', '2023-09-10', '789 Oak St, Villagetown', 'Shoplifting incident at a mall', 'Closed'); 
 
INSERT INTO Victim (VictimID, CrimeID, Name,age, ContactInfo, Injuries) VALUES 
    (1, 1, 'John Doe',53, 'johndoe@example.com', 'Minor injuries'), 
    (2, 2, 'Jane Smith',32, 'janesmith@example.com', 'Deceased'), 
	(3, 3, 'Alice Johnson',41, 'alicejohnson@example.com', 'None'); 

INSERT INTO Suspect (SuspectID, CrimeID, Name, Description, CriminalHistory) VALUES 
(1, 1, 'Robber 1', 'Armed and masked robber', 'Previous robbery convictions'), 
(2, 2, 'Unknown', 'Investigation ongoing', NULL), 
(3, 3, 'Suspect 1', 'Shoplifting suspect', 'Prior shoplifting arrests'); 



select *from Crime
select *from Victim
select *from Suspect
```
![image](https://github.com/user-attachments/assets/844b2422-1643-4634-a716-be0d31788b9d)

--1. Select all open incidents.
```sql
Select *from Crime
where status = 'open'
```
![image](https://github.com/user-attachments/assets/70d2d86d-a25a-4c65-b74b-8ba01a0fe6a0)

--2 Find the total number of incidents. 
```sql
Select Count(*) as Total_Incidents from crime
```
![image](https://github.com/user-attachments/assets/6bcef51e-a537-4358-a19e-acf5d52e4956)

--3 List all unique incident types.
```sql
Select Distinct IncidentType from Crime

```
![image](https://github.com/user-attachments/assets/c05e0250-3a93-419b-964b-c366f62f7122)

--4 Retrieve incidents that occurred between '2023-09-01' and '2023-09-10'.
```sql
Select *from Crime
where IncidentDate between '2023-09-01' and '2023-09-10'
```
![image](https://github.com/user-attachments/assets/d60cb7cb-023c-4912-a67b-1c496f6223d3)

--5 List persons involved in incidents in descending order of age. 
```sql
Select VictimID,name,age from Victim
Order by age DESC
```
![image](https://github.com/user-attachments/assets/82cbb8d9-5222-4106-be91-90eb2b745a3b)

--6 Find the average age of persons involved in incidents. 
```sql
Select AVG(age) as AVG_Age from victim
```
![image](https://github.com/user-attachments/assets/75868583-11b4-40e6-b0b0-9239fe568326)

--7  List incident types and their counts, only for open cases.
```sql
Select IncidentType, COUNT(*) as IncidentCount
from Crime
where Status = 'Open'
GROUP BY IncidentType;
```
![image](https://github.com/user-attachments/assets/b553e6a0-4b32-4160-a319-19188ec002bf)

--8  Find persons with names containing 'Doe'. 
```sql
Select name from victim
where name like '%doe%'
```

![image](https://github.com/user-attachments/assets/23b014d6-2bf3-43d0-a3aa-58ccd3bcd2b3)

--9 Retrieve the names of persons involved in open cases and closed cases.
```sql
Select v.name,c.status 
from Victim v
join crime c on v.CrimeID = c.CrimeID
where c.status in ('Open', 'Closed')
```

![image](https://github.com/user-attachments/assets/fbd14a0c-8d7a-4be7-b939-ec003b0abd33)


--10  List incident types where there are persons aged 30 or 35 involved. 
```sql
select v.name,v.age,c.incidenttype
from victim v
join crime c
on v.crimeId = c.crimeID
where v.age in (32,35)
```

![image](https://github.com/user-attachments/assets/fe3e0e17-8a40-49b7-8c20-3c32000c98cf)

--11 Find persons involved in incidents of the same type as 'Robbery'. 
```sql
select v.Name, 'Victim' AS Role
from Victim v
JOIN Crime c on v.CrimeID = c.CrimeID
where c.IncidentType = 'Robbery'
UNION
select s.Name, 'Suspect' AS Role
from Suspect s
JOIN Crime c on s.CrimeID = c.CrimeID
where c.IncidentType = 'Robbery';
```

![image](https://github.com/user-attachments/assets/fbeff201-4c80-45cb-a4d8-9cd3f68b531a)

--12  List incident types with more than one open case. 
```sql
select IncidentType, count(*) as open_case_count
from Crime 
where status = 'open'
group by IncidentType
having count(*) >=1
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
select c.*,v.name,v.ContactInfo,s.name,s.CriminalHistory
from Crime c
left join victim v on c.crimeId = v.crimeID
left join Suspect s on c.crimeId = s.crimeID
```

![image](https://github.com/user-attachments/assets/104d25f8-8a27-4fcd-ba7a-08c964868250)

--15  Find incidents where the suspect is older than any victim.
```sql
alter table suspect
add age int 

update Suspect
set age = 55
where SuspectID = 3

Select s.*
from Suspect s
join victim v on s.crimeID = v.crimeID
where s.age>v.age
```

![image](https://github.com/user-attachments/assets/c1966fa6-e853-4fec-82d1-db07a42ad692)



--16  Find suspects involved in multiple incidents:
```sql
select s.Name, COUNT(*) AS IncidentCount
from Suspect s
GROUP BY s.Name
having COUNT(*) >= 1;
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

![image](https://github.com/user-attachments/assets/a655893c-a766-47f0-a8e9-193b72483347)

--20  List all suspects who have been involved in incidents with incident types 'Robbery' or 'Assault'
```sql
select s.*, c.incidentType
from crime c
join Suspect s on c.crimeId = s.crimeID
where c.incidentType in ('Robbery','Assualt')
```

![image](https://github.com/user-attachments/assets/e25d12e3-9fb5-4e19-b8f2-51ff59a35bd7)
