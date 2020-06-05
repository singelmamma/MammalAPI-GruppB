## **GetAllFamilies:**

1. False - False = works

2. True - False = works

3. False - True = works

4. True - True = works

   

## **GetFamilyById:**

##### Id used = 1

1. False - False = works
2. True - False = works
3. False - True = works
4. True - True = works

##### Id used = 5

1. False - False = works
2. True - False = works
3. False - True = works
4. True - True = works

##### Id used = 11 (Family I just posted during this test, lacks relationships, they're null)

1. False - False = works

2. True - False = works

3. False - True = works

4. True - True = works

   

## GetFamilyByName:

##### Name used = "Phocidae"

1. False - False = works
2. True - False = works
3. False - True = works
4. True - True = works

##### Name used = "Ursidae"

1. False - False = works
2. True - False = works
3. False - True = works
4. True - True = works

##### Name used = "FamilyTestPost" (Family I just posted during this test, lacks relationships, they're null)

1. False - False = works

2. True - False = works

3. False - True = works

4. True - True = works

   

## PostFamily:

##### 1.

{
  "name": "FamilyTestPost",
}
= Works

##### 2.

{
  "FamilyId": 12,
  "name": "FamilyTestPost",
}
= Should not work and does not work



## PutFamily:

##### 1.

{
  "familyID": 11,
  "name": "string",
}
= Works

##### 2.

{
  "name": "string",
}
= Does not work and should not work (500 error)



## DeleteFamily:

1. FamilyId: 11 = Works
2. FamilyId: 57 = Does not work and should not work (404 not found error)


Authentication: Works on all applicable requests for usr: Hampus, pswd: "The standard password"