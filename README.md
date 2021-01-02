# FarmTracker-services
- [See Project Page](https://github.com/users/Sharaido/projects/4)
## Database
- [See Database ER Diagram](https://github.com/Sharaido/FarmTracker-files/blob/main/database/db.png)
- [See Database Script (MSSQL)](https://github.com/Sharaido/FarmTracker-files/blob/main/database/farmTracker.sql)
## Functions
- [Get unique code to sign up](#Get-unique-code-to-sign-up)
- [Sign Up](#Sign-Up)
- [Username check](#Username-check)
- [Email check](#Email-check)
- [Sign In](#Sign-In)
- [Change MemberType](#Change-MemberType)
- [Inactivate a session](#Inactivate-a-session)
- [Create Farm](#Create-Farm)
- [Get user's farms](#Get-users-farms)
- [Delete farm](#Delete-farm)
- [Create property for a farm](#Create-property-for-a-farm)
- [Get The Farm's Properties](#Get-The-Farms-Properties)
- [Get the property](#Get-the-property)
- [Delete farm property](#Delete-farm-property)
- [Create Entity for a farm property](#Create-Entity-for-a-farm-property)
- [Get Entities for a farm property](#Get-Entities-for-a-farm-property)
- [Get the entity](#Get-the-entity)
- [Delete fp entity](#Delete-fp-entity)
- [Create COPValue for an entityOfFP](#Create-COPValue-for-an-entityOfFP)
- [Get COPValues for an entityOfFP](#Get-COPValues-for-an-entityOfFP)
- [Create detail for a entityOfFP](#Create-detail-for-a-entityOfFP)
- [Create Income](#Create-Income)
- [Get farm's incomes](#Get-farms-incomes)
- [Create Expense](#Create-Expense)
- [Get farm's expenses](#Get-farms-expenses)
- [Get income and expenses for a farm](#Get-income-and-expenses-for-a-farm)
- [Delete income or expense](#Delete-income-or-expense)
- [Add FarmEntity](#Add-FarmEntity)
- [Get FarmEntities](#Get-FarmEntities)
- [Delete FarmEntity](#Delete-FarmEntity)
- [Get Adds](#Get-Adds)
- [Get Add](#Get-Add)
- [Get User Adds](#Get-User-Adds)
- [Create Add](#Create-Add)
- [Delete Add](#Delete-Add)
- [Add Picture for Add](#Add-Picture-for-Add)
- [Get Pictures for Add](#Get-Pictures-for-Add)
- [Get CopValues for Add](#Get-CopValues-for-Add)
- [Add CopValues for Add](#Add-CopValues-for-Add)

### Members Controller
#### Get unique code to sign up
- A user can request 5 times to unique code in 5 minutes from same IP.
- Every unique code can use one times.
- Every unique code have expiration date. 
##### Request 
	GET http://localhost:8181/api/Members/GetNewUCodeForSignUp
##### Response
	{
		"guc": "{GUC}",
		"forUuid": null,
		"forIp": "198.162.2.1",
		"createdDate": "2020-11-01T11:18:28.623",
		"expirationDate": "2020-11-01T11:23:28.623",
		"isValid": true,
		"ctuid": 4,
	}
#### Sign Up
- Sign up request needs a unique code for created signing up.
##### Request Body
	{
		"Username": "doguskar",
		"Password": "***********",
		"Email": "example@example.com",
		"Name": "Doğuş",
		"Surname": "Kar",
		"Guc": "{GUC}"
	}
##### Request 
	POST http://localhost:8181/api/Members/SignUp 
##### Response
	{
		"username": "doguskar",
		"name": "Doğuş",
		"surname": "Kar"
	}
#### Username check
##### Request 
	GET http://localhost:8181/api/Members/IsUsedUsername/{username}
##### Response
	{ false }
#### Email check
##### Request 
	GET http://localhost:8181/api/Members/IsUsedEmail/{email}
##### Response
	{ false }
#### Sign In
- A user can request to sign in  5 times in 5 minutes.
##### Request Body
	{
		"SignInKey": "doguskar",
		"Password": "***********"
	}
##### Request 
	POST http://localhost:8181/api/Members/SignIn
##### Response
	{
		"result": true,
		"showCaptcha": false,
		"invalidSignInKey": false,
		"invalidPassword": false,
		"tooManyAttempts": false,
		"sessionTerminated": false,
		"redirectAddress": null,
		"token": "*******************",
		"expiration": "2020-11-01T14:25:46Z"
	}

#### Change MemberType
##### Request Body
	{
		"uuid": "70F13C78-E515-EB11-A495-201A06A548BB",
		"mtuid": 3
	}
##### Request 
	PUT http://localhost:8181/api/Members/MemberType/
##### Response
	true

#### Inactivate a session
- This function required authorization

##### Request 
	POST http://localhost:8181/api/Members/InactiveteSession/{SUID}

### Farms Controller
- Farms controller needs authorization. 

#### Create Farm
##### Request Body
	{
		"Name": "Farm",
		"Description": "desc desc desc desc desc desc desc"
	}
##### Request 
	POST http://localhost:8181/api/Farms/
##### Response
	{
		"fuid": "{FUID}",
		"name": "Farm 4",
		"description": "desc desc desc desc desc desc desc"
	}

#### Get user's farms
##### Request 
	GET http://localhost:8181/api/Farms/
##### Response
	[
	    {
	        "fuid": "{FUID}",
	        "name": "Farm 4",
	        "description": "desc desc desc desc desc desc desc",
	        "createdByUuid": "{UUID}",
	        "createdDate": "2020-11-01T11:32:46.907",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null,
	        "createdByUu": null,
	        "deletedByUu": null
	    }
	]
#### Delete farm
##### Request 
	DELETE http://localhost:8181/api/Farms/{FUID}
##### Respone
	{ true } 

#### Create property for a farm
##### Request Body
	{
	    "Name": "Ranch",
	    "Description": "desc desc desc desc desc desc",
	    "CUID": 2,
	    "FUID": "{FUID}"
	}
##### Request
	POST http://localhost:8181/api/Farms/Properties/
##### Response
	{
	    "fuid": "{FUID}",
	    "puid": "{PUID}",
	    "cuid": 2,
	    "name": "Ranch",
	    "description": "desc desc desc desc desc desc"
	}
#### Get The Farm's Properties
##### Request
	GET http://localhost:8181/api/Farms/Properties/{FUID}
##### Response
	[
	    {
	        "puid": "{PUID}",
	        "name": "Ranch",
	        "description": "desc desc desc desc desc desc",
	        "cuid": 2,
	        "fuid": "{FUID}",
	        "createdByUuid": "{UUID}",
	        "createdDate": "2020-11-01T11:50:20.39",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null,
	        "createdByUu": null,
	        "deletedByUu": null
	    }
	]
#### Get the property
##### Request
	GET http://localhost:8181/api/Farms/Properties/{FUID}/{PUID}
##### Response
	{
		"puid": "9f82f778-a61b-eb11-a49c-201a06a548bb",
		"name": "Field 1",
		"description": "desc desc desc desc desc desc",
		"cuid": 1,
		"fuid": "b0afcd1b-9d1b-eb11-a49c-201a06a548bb",
		"createdByUuid": "e07e65e7-1815-eb11-a494-201a06a548bb",
		"createdDate": "2020-10-31T18:25:38.29",
		"deletedFlag": false,
		"deletedDate": null,
		"deletedByUuid": null,
		"createdByUu": null
	}
#### Delete farm property
##### Request 
	DELETE http://localhost:8181/api/Farms/Properties/{PUID}
##### Respone
	{ true } 
	
#### Create Entity for a farm property
##### Request Body
	{
	    "CUID": 8,
	    "PUID": "{PUID}",
	    "ID": null,
	    "Name": "Blue Dolphin",
	    "Description": null,
	    "Count": 1,
	    "PurchasedDate": null,
	    "Cost": 0
	}
##### Request 
	POST http://localhost:8181/api/Farms/Properties/Entities/
##### Respone
	{
	    "euid": "{EUID}",
	    "puid": "{PUID}",
	    "cuid": 8,
	    "id": null,
	    "name": "Blue Dolphin",
	    "description": null,
	    "count": 1,
	    "purchasedDate": null,
	    "cost": 0.0000
	}
	
#### Get Entities for a farm property
##### Request 
	GET http://localhost:8181/api/Farms/Properties/Entities/{FarmProperyID}
##### Respone 
	[
	    {
	        "euid": "{EUID}",
	        "cuid": 8,
	        "puid": "{PUID}",
	        "id": null,
	        "name": "Blue Dolphin",
	        "description": null,
	        "count": 1,
	        "purchasedDate": null,
	        "cost": 0.0000,
	        "soldFlag": false,
	        "soldDate": null,
	        "soldPrice": null,
	        "soldDetail": null,
	        "soldByUuid": null,
	        "createdDate": "2020-11-01T11:58:47.27",
	        "createdByUuid": "{UUID}",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null,
	        "createdByUu": null
	    }
	]

#### Get the entity
##### Request 
	GET http://localhost:8181/api/Farms/Properties/Entities/{FarmProperyID}/{EntityID}
##### Respone 
	{
		"euid": "{EUID}",
		"cuid": 8,
		"puid": "{PUID}",
		"id": null,
		"name": "Blue Dolphin",
		"description": null,
		"count": 1,
		"purchasedDate": null,
		"cost": 0.0000,
		"soldFlag": false,
		"soldDate": null,
		"soldPrice": null,
		"soldDetail": null,
		"soldByUuid": null,
		"createdDate": "2020-11-01T11:58:47.27",
		"createdByUuid": "{UUID}",
		"deletedFlag": false,
		"deletedDate": null,
		"deletedByUuid": null,
		"createdByUu": null
	}	
#### Delete fp entity
##### Request 
	DELETE http://localhost:8181/api/Farms/Properties/Entities/{EUID}
##### Respone
	{ true }

#### Create COPValue for an entityOfFP
##### Request Body
	{
	    "EUID": "{EUID}",
	    "PUID": 2,
	    "Value": "20"
	}
##### Request 
	POST http://localhost:8181/api/Farms/Properties/Entities/COPValues/
##### Respone 
	{
	    "euid": "{EUID}",
	    "puid": 2,
	    "value": "20"
	}

#### Get COPValues for an entityOfFP
##### Request 
	GET http://localhost:8181/api/Farms/Properties/Entities/COPValues/{EUID}
##### Respone 
	[
	    {
	        "euid": "{EUID}",
	        "puid": 2,
	        "value": "20"
	    }
	]

#### Create detail for a entityOfFP
##### Request Body
	{
	    "EUID": "{EUID}",
	    "Name": "Detail",
	    "Description": "Description Description",
	    "Cost": null,
	    "RemainderDate": null
	}
##### Request 
	POST http://localhost:8181/api/Farms/Properties/Entities/Details/
##### Respone 
	{
	    "duid": "{DUID}",
	    "euid": "{EUID}",
	    "name": "Detail",
	    "description": "Description Description",
	    "cost": null,
	    "remainderDate": null
	}

#### Get entityOfFP's details
##### Request Body
##### Request 
	GET http://localhost:8181/api/Farms/Properties/Entities/Details/{EUID}
##### Respone 
	[
	    {
	        "duid": "{DUID}",
	        "euid": "{EUID}",
	        "name": "Detail",
	        "description": "Description Description",
	        "cost": null,
	        "remainderDate": null,
	        "remainderCompletedFlag": null,
	        "remainderCompletedDate": null,
	        "remainderCompletedByUuid": null,
	        "createdDate": "2020-11-01T07:57:00.523",
	        "createdByUuid": "{UUID}",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null
	    }
	]

#### Create Income
##### Request Body
	{
	    "FUID": "{FUID}",
	    "Date": "2020-11-01",
	    "Head": "Income",
	    "Description": null,
	    "Cost": 95
	}
##### Request 
	POST http://localhost:8181/api/Farms/Incomes/
##### Respone 
	{
	    "ieuid": "{IEUID}",
	    "fuid": "{FUID}",
	    "date": "2020-11-01T00:00:00",
	    "head": "Income",
	    "description": null,
	    "cost": 95.0000,
	    "incomeFlag": true
	}

#### Get farm's incomes
##### Request 
	GET http://localhost:8181/api/Farms/Incomes/{FUID}
##### Respone 
	[
	    {
	        "ieuid": "{IEUID}",
	        "fuid": "{FUID}",
	        "incomeFlag": true,
	        "date": "2020-11-01T00:00:00",
	        "head": "Income",
	        "description": null,
	        "cost": 95.0000,
	        "createdByUuid": "{UUID}",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null
	    }
	]
#### Create Expense
##### Request Body
	{
	    "FUID": "{FUID}",
	    "Date": "2020-11-01",
	    "Head": "Expense",
	    "Description": null,
	    "Cost": 95
	}
##### Request 
	POST http://localhost:8181/api/Farms/Expenses/
##### Respone 
	{
	    "ieuid": "{IEUID}",
	    "fuid": "{FUID}",
	    "date": "2020-11-01T00:00:00",
	    "head": "Expense",
	    "description": null,
	    "cost": 95.0000,
	    "incomeFlag": false
	}

#### Get farm's expenses
##### Request 
	GET http://localhost:8181/api/Farms/Expenses/{FUID}
##### Respone 
	[
	    {
	        "ieuid": "{IEUID}",
	        "fuid": "{FUID}",
	        "incomeFlag": false,
	        "date": "2020-11-01T00:00:00",
	        "head": "Expense",
	        "description": null,
	        "cost": 95.0000,
	        "createdByUuid": "{UUID}",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null
	    }
	]

#### Get income and expenses for a farm
##### Request 
	GET http://localhost:8181/api/Farms/IncomeAndExpenses/{FUID}
##### Respone 
	[
	    {
	        "ieuid": "{IEUID}",
	        "fuid": "{FUID}",
	        "incomeFlag": true,
	        "date": "2020-11-01T00:00:00",
	        "head": "Income",
	        "description": null,
	        "cost": 95.0000,
	        "createdByUuid": "{UUID}",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null
	    },
	    {
	        "ieuid": "{IEUID}",
	        "fuid": "{FUID}",
	        "incomeFlag": false,
	        "date": "2020-11-01T00:00:00",
	        "head": "Expense",
	        "description": null,
	        "cost": 95.0000,
	        "createdByUuid": "{UUID}",
	        "deletedFlag": false,
	        "deletedDate": null,
	        "deletedByUuid": null
	    }
	]
#### Delete income or expense
##### Request 
	DELETE http://localhost:8181/api/Farms/IncomeAndExpenses/{IEUID}
##### Respone
	{ true } 

#### Add FarmEntity
##### Request Body
	{
		"name": "Fish food",
		"cuid": "22",
		"fuid": {FUID},
		"count": 5
	}
##### Request 
	POST http://localhost:8181/api/Farms/FarmEntities/
##### Respone 
	{
		"euid": {EUID},
		"name": "Fish food",
		"cuid": 22,
		"fuid": {FUID},
		"count": 5,
		"createdByUuid": {UUID},
		"createdDate": "2020-12-20T10:11:45.737"
	}

#### Get FarmEntities
##### Request 
	GET http://localhost:8181/api/Farms/FarmEntities/{FUID}
##### Respone 
	[
		{
			"euid": {EUID},
			"name": "Fish food",
			"cuid": 22,
			"fuid": {FUID},
			"count": 5,
			"createdByUuid": {UUID},
			"createdDate": "2020-12-20T10:11:45.737"
		}
	]

#### Delete FarmEntity
##### Request 
	DELETE http://localhost:8181/api/Farms/FarmEntities/{EUID}
##### Respone
	{ true } 

### Adds

#### Get Adds
##### Request 
	GET http://localhost:8181/api/Adds/
##### Respone 
	[
		{
			"auid": {AUID},
			"cuid": 22,
			"name": "Test",
			"description": "desc",
			"price": 10.0000,
			"discount": null,
			"createdByUuid": {UUID},
			"createdDate": "2020-12-20T08:26:34.88",
			"confirmedFlag": false,
			"confirmedByUuid": null,
			"publishedFlag": false,
			"publishedDate": null,
			"pictures": [
				{
					"puid": "42204165-ba42-eb11-a4e3-201a06a548bb",
					"address": "image.jpg",
					"auid": "b2bdad11-9d42-eb11-a4e3-201a06a548bb"
				}
			],
			"addCopvalues": [
				{
					"auid": {AUID},
					"puid": 3,
					"value": "Healty",
					"pu": null
				}
			],
		}
	]
#### Get Add
##### Request 
	GET http://localhost:8181/api/Adds/{AUID}
##### Respone 
	{
		"auid": {AUID},
		"cuid": 22,
		"name": "Test",
		"description": "desc",
		"price": 10.0000,
		"discount": null,
		"createdByUuid": {UUID},
		"createdDate": "2020-12-20T08:26:34.88",
		"confirmedFlag": false,
		"confirmedByUuid": null,
		"publishedFlag": false,
		"publishedDate": null,
        "pictures": [
            {
                "puid": "42204165-ba42-eb11-a4e3-201a06a548bb",
                "address": "image.jpg",
                "auid": "b2bdad11-9d42-eb11-a4e3-201a06a548bb"
            }
        ],
		"addCopvalues": [
			{
				"auid": {AUID},
				"puid": 3,
				"value": "Healty",
				"pu": null
			}
		]
	}
	

#### Get User Adds
##### Request 
	GET http://localhost:8181/api/Adds/User/{UUID}
##### Respone 
	[
		{
			"auid": {AUID},
			"cuid": 22,
			"name": "Test",
			"description": "desc",
			"price": 10.0000,
			"discount": null,
			"createdByUuid": {UUID},
			"createdDate": "2020-12-20T08:26:34.88",
			"confirmedFlag": false,
			"confirmedByUuid": null,
			"publishedFlag": false,
			"publishedDate": null,
			"pictures": [
				{
					"puid": "42204165-ba42-eb11-a4e3-201a06a548bb",
					"address": "image.jpg",
					"auid": "b2bdad11-9d42-eb11-a4e3-201a06a548bb"
				}
			]
		}
	]

#### Create Add
Creating add requieres authentication
##### Request Body
	{
		"name": "test",
		"desc": "desc",
		"price": 500,
		"cuid": 22
	}
##### Request 
	POST http://localhost:8181/api/Adds
##### Respone 
	{
		"auid": {AUID},
		"cuid": 22,
		"name": "test",
		"description": null,
		"price": 500.0000,
		"discount": null,
		"createdByUuid": {UUID},
		"createdDate": "2020-12-20T10:00:45.83",
		"confirmedFlag": false,
		"confirmedByUuid": null,
		"publishedFlag": false,
		"publishedDate": null
	}

	
#### Delete Add
Deleting add requieres authentication
##### Request 
	DELETE http://localhost:8181/api/Adds/{AUID}
##### Respone
	{ true } 

#### Add Picture for Add
Adding picture requieres authentication
##### Request Body
	{
		"address": "image.jpg",
		"auid": {AUID}
	}
##### Request 
	POST http://localhost:8181/api/Adds
##### Respone 
	true
	
#### Get Pictures for Add
##### Request 
	GET http://localhost:8181/api/Adds/Pictures/{AUID}
##### Respone 
	[
		{
			"puid": {PUID},
			"address": "image.jpg",
			"auid": {AUID}
		}
	]

#### Get CopValues for Add
##### Request 
	GET http://localhost:8181/api/Adds/COPValues/{AUID}
##### Respone 
	[
		{
			"auid": {AUID},
			"puid": {PUID},
			"value": "30"
		}
	]

#### Add CopValues for Add
Adding CopValues requieres authentication
##### Request Body
	{
		"AUID": {AUID},
		"PUID": {PUID},
		"Value": "30"
	}
##### Request 
	POST http://localhost:8181/api/Adds/COPValues/
##### Respone 
	true