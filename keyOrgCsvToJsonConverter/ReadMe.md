# keyOrgCsvToJsonConverter
2023/02/05, by Ruby Felton

## Purpose
keyOrgCsvToJsonConverter.exe is a 64bit custom conversion tool to transfer csv into json.
The conversion follows K3-KeyOrg specific logic.  See keyOrganizer_template.xlsx for sample and save it as csv.

## Execution Syntax
keyOrgCsvToJsonConverter.exe inputfile outputfile

>Example:
>keyOrgCsvToJsonConverter.exe .\input.csv .\output.json

## Features
- return code of non-zero in case of runtime error
- skip the records for when line is not valid. Exmaple: for groups, must have category and name. for items, needs plu and department.
- can be run within the sale box as part of zip package.
- linux version available, upon request

## Input File
CSV Input follows this format (comma separated):
id- unique index or primary key
category - must be defined, either 'groups' or 'items'
name - name for the group button.  Optional for category 'items'.  For item, if name is defined, this is used instead of PLTE
parent - parent group id.  If 0 or empty, means root or top level.
plu - plu number (only applies to 'items')
department - department number (only applies to 'items')
defText - if PLU doesnt exist in PLST, this field will appear on the button. (not proven to work yet)
active - true or false.  if empty, defaults to true.

Sample Data: [input.csv]
```csv
id,category,name,parent,plu,department,defText,active
1,groups,deli,0,,,,TRUE
2,groups,meat,1,,,,TRUE
3,groups,cheese,1,,,,FALSE
4,groups,bakery,0,,,,
5,items,turkey,2,1234,1,smoked turkey,TRUE
6,items,dounut,4,345,2,glazed dounut,
7,items,gift card,,98,1,sdjfghwi,
```

## Output File
Sample Output: [output.json]
```json
[
  [
    {
      "id": 1,
      "parent": 0,
      "name": "deli",
      "groups": [
        {
          "id": 2,
          "parent": 1,
          "name": "meat",
          "groups": [],
          "items": [
            {
              "id": 5,
              "parent": 2,
              "plu": "1234",
              "department": "1",
              "name": "turkey",
              "defText": "smoked turkey",
              "active": true
            }
          ],
          "active": true
        },
        {
          "id": 3,
          "parent": 1,
          "name": "cheese",
          "groups": [],
          "items": [],
          "active": false
        }
      ],
      "items": [],
      "active": true
    },
    {
      "id": 4,
      "parent": 0,
      "name": "bakery",
      "groups": [],
      "items": [
        {
          "id": 6,
          "parent": 4,
          "plu": "345",
          "department": "2",
          "name": "dounut",
          "defText": "glazed dounut",
          "active": true
        }
      ],
      "active": true
    }
  ],
  [
    {
      "id": 7,
      "parent": 0,
      "plu": "98",
      "department": "1",
      "name": "gift card",
      "defText": "sdjfghwi",
      "active": true
    }
  ]
]
```