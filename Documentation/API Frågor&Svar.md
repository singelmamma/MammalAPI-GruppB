##ROOT

http://serveradress.com/api/v1.0

##ENDPOINTS

**/mammals					GET**

Returns generic info about all mammals

```javascript
[
	{
		MammalID: 1234,
		Name: “Blue Whale”
	},
	{
		MammalID: 2345,
		Name: “Seal”
	},
	{
		MammalID: 3456,
		Name: “Dugong”
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

**/mammals-data				GET**

Returns data; mamalsID, name,latin name, length and weight for all mammals

```javascript
[
{
MammalD: 1,
Name: “Blue Whale”,
LatinName:”Balaenoptera musculus”,
Length:40,
Weight:3500
},
{
MammalD: 2,
Name: “Turqoise Whale”,
LatinName:”Balaenoptera musculus”,
Length:5,
Weight:250
}
]
```

**/mammals-data<id>				GET**
	
Returns data; name,latin name, length and weight about one specific mammal

```javascript
{
MammallD: 1,
Name: “Blue Whale”,
LatinName:”Balaenoptera musculus”,
Length:40,
Weight:3500
}
```

**/mammals?family=<familyName>			GET**

Returns all mammals in a given family

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

**/mammals?family=<habitatID>		GET**

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

**/mammals?habitat=<habitatName>		GET**

Returns all the mammals in a given habitat by habitat name

**/mammals?habitat=<habitatID>			GET**
	
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

/habitat?name=<name>
Returns the ID of a habitat
{
	ID: 666
}

/habitat/<id>
Returns the name of a habitat
{
	Name: “Indian Ocean”
}

/family?name=<name>&habitat=<name>
Returns mammals living in the specified habitat from a specific family of mammals

{
	mammals: 
[
	{
	Name: “Elephant Seal”,
	ID: 222
},
	{
	Name: “Antarctic Fur Seal”,
	ID: 213
},
	{
	Name: “ Crabeater Seal”,
	ID: 422
}
]
}

