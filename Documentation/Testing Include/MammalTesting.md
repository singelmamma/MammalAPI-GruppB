### Get all Mammals

* false. false. false = works
* true. true. true = works men rundgång
* true. false. false. = works
* true. true. false = rundgång
* true. false. true = works
* false. true. false = inga länkar men rundgång
* false. false. true = works
* false. true. true = works men rundgång



### Get mammal by id = 4

* false. false. false = works
* true. true. true = works
* true. false. false. = Crash! {in HateoasControllerBase familyDto was null}
* true. true. false = works
* true. false. true = Crash! {in HateoasControllerBase familyDto was null}
* false. true. false =  Works men rundgång
* false. false. true = works
* false. true. true = works men rundgång



### Get mammal by Name = Blue Whale

* false. false= works
* true. true = works men rundgång i family, den visar dess mamal igen
* true. false = {in HateoasControllerBase familyDto was null}
* false. true = works men rundgång i family, den visar dess mamal igen



### Get Mammal by Habitat Id 5

* false. false. false = works
* true. true. true = works
* true. false. false = works
* true. true. false = works
* true. false .true = works
* false. true. false = works men rundgång i family, den visar dess mamal igen
* false. false. true = works
* false. true. true = works men rundgång i family, den visar dess mamal igen



### Get Mammal by Habitat Name = Atlantic Ocean

* true = works

* false = works



### Get mammals By FamilyId 1

* true. true. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)

* false. false. false = works
* true. false. false = works
* true. true. false =  works but doesnt show the links to the mammal!
* true. false. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* false. true. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary.)
* false. false. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary.)
* false. true. false = works



### Get mammal by Family Name "Phocidae"

* true. true. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* false. false. false = works
* true. false. false = Works men inga länkar
* true. true. false =  works men får bara länkarna till Habitat inte Mammals
* true. false. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* false. true. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* false. false. true =**fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* false. true. false = works



### Get mammals lifespan by 1 and 50

* true. true. true = **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* false. false. false = works
* true. false. false = works
* true. true. false =  **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* true. false. true = works
* false. true. true =  **fails**(Something went wrong: The given key 'Phocidae' was not present in the dictionary)
* false. false. true = works
* false. true. false = Something went wrong: The given key 'Phocidae' was not present in the dictionary.



### Post mammal

* mammalId = 79;
* name = "TestusMammulus"
  * Fail (Database Failure : An error occurred while updating the entries. See the inner exception for details.)



### Put Mammal

* mammalId = 11;

* Name = Testus;
  * Works (Changed Back to Blue Whale)





### Delete Mammal by Id 1 

* This works fine.
  * Down belove is the information about the animal i Deleted, but because i cant post it, i have to save it here

* ```
  {    "mammalID": 1,    "name": "Leopard Seal",    "children": 0,    "length": 3.5,    "weight": 600,    "latinName": "Hydrurga leptonyx",    "lifespan": 26,    "habitats": [      {        "habitatID": 5,        "name": "Arctic Ocean",        "mammal": [],        "links": [          {            "href": "https://localhost:44358/api/v1.0/habitat",            "rel": "all",            "type": "GET"          },          {            "href": "https://localhost:44358/api/v1.0/habitat/5",            "rel": "_self",            "type": "GET"          },          {            "href": "https://localhost:44358/api/v1.0/habitat/arctic%20ocean",            "rel": "self",            "type": "GET"          }        ]      }    ],    "family": null,    "links": [      {        "href": "https://localhost:44358/api/v1.0/mammals",        "rel": "all",        "type": "GET"      },      {        "href": "https://localhost:44358/api/v1.0/mammals/1",        "rel": "_self",        "type": "GET"      },      {        "href": "https://localhost:44358/api/v1.0/mammals/leopard%20seal",        "rel": "_self",        "type": "GET"      }    ]  }
  ```

### 



