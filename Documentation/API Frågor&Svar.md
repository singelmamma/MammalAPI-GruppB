## ROOT

http://serveradress.com/api/v1.0

## ENDPOINTS - Mammal



**/mammal/getall				GET**

Returns generic info about all mammals



```javascript
[
	{
		MammalID: 1234,
		Name: “Blue Whale”,
		LatinName: "Latin Latin",
		Lenght: 30.2,
		Weight: 27000
	},
	{
		MammalID: 2345,
		Name: “Seal”,
		LatinName: "Seal in Latin",
		Lenght: 1.8,
		Weight: 47.2
	}
]
```



**/mammal/<id>				GET**

Returns generic info about one specific mammal



```javascript
{
	ID: 1,
	Name: “Blue Seal”,
	Length: 132,
	Weight: 80,
	LatinName: “Latino Name”,
	Lifespan: 9,
	HabitatID: 2,
	FamilyID: 4
}
```



**/mammal/habitat/habitat<habitatName>		GET**

Returns all the mammals in a given habitat by habitat name

**/mammal/?habitatId=<habitatID>			GET**

Returns all animals in a given habitat by habitat ID



```javascript
[
	{
		“MammalID”: 159,
		“Name”: “Killerwhale”
	},
	{
		“MammalID”: 754,
		“Name”: “Blue Whale”
	},
	{
		“MammalID”: 157,
		“Name”: “Harbor Seal”
	}
]
```



**/mammal/lifespan/fromYear=<fromYear>&toYear=<toYear>

Returns all mammals by a given lifespan



```javascript
[
	{
		MammalID: 156,
		Name: “Blue Whale”
        Lifespan: 20
	},
	{
		MammalID: 784,
		Name: “Crabeater Seal”
        Lifespan: 30
	}
]
```



**/mammal/byfamilyname/<familyName>			GET**
	
Returns all mammals in a given family by name
	
**/mammal/byfamilyid/<familyId>				GET**

Returns all mammals in a given family by id



```javascript
[
	{
		MammalID: 156,
		Name: “Blue Whale”
	},
	{
		MammalID: 784,
		Name: “Crabeater Seal”
	}
]
```



## ENDPOINTS - Family



**/family/byname/<familyname>			GET**
	
Returns FamilyId and FamilyName by name

**/family/byid/<familyid>			GET**

Returns FamilyId and FamilyName by Id



```javascript
{
	id: 1,
	name: "Mustelidae"
}
```



**/family/all					GET**

Returns all families



```javascript
[
	{
		id: 1,
		name: "Phocidae"
	},
	{
		"id": 2,
		"name": "Balaenopteridae"
	}
]
```



## ENDPOINTS - Habitat



**/habitat/?habitatName=<habitatName>		GET**	
	
Returns the habitat info by name

**/habitat/<id>					GET**
	
Returns the habitat info by id



```javascript
{
    "id": 1,
    "name": "Pacific Ocean"
}
```



**/habitat/all					GET**

Returns all Habitats



```javascript
[
	{
		id: 1,
        	"name": "Pacific Ocean"
	},
	{
		"id": 2,
		"name": "Atlantic Ocean"
	}
]
```
