#  Laboration 4 - Webbtjänster/API'er.

## Syfte: 

- Kunna skapa API/REST-webbtjänst med ASP.net Core som ska innehålla stöd för CRUD (Create Read Update Delete).


###   Innehåll: 

- Artist API 

Med Postman eller Thunder Client man kan testa API och CRUD-funktionalitet med nedstående:
       
| Domain | Method    | URI                   | BESKRIVNING  |
| ------------|-----------|--------|--------| 
|        | GET       | api/Artist             | Hämta ut all data från tabellen.                |
|        | GET       | api/Artist/ID          | Hämta ut en rad från tabellen med ett givet id. |
|        | POST      | api/Artist             | Lägga till data till tabellen                   |
|        | PUT       | api/Artist/ID          | Uppdatera data för en rad med ett givet id.     |      
|        | DELETE    | api/Artist/ID          | Radera en rad i tabellen med ett givet id.      |


Artist data skickas som JSON data till API i följande format:

```
{
    "name":" name of the artist",
}
```
- Category API 

Med Postman eller Thunder Client man kan testa API och CRUD-funktionalitet med nedstående:

| Domain | Method    | URI                   | BESKRIVNING  |
| ------------|-----------|--------|--------| 
|        | GET       | api/Category             | Hämta ut all data från tabellen.                |
|        | GET       | api/Category/ID          | Hämta ut en rad från tabellen med ett givet id. |
|        | POST      | api/Category             | Lägga till data till tabellen                   |
|        | PUT       | api/Category/ID          | Uppdatera data för en rad med ett givet id.     |      
|        | DELETE    | api/Category/ID          | Radera en rad i tabellen med ett givet id.      |


Category data skickas som JSON data till API i följande format:

```
{
    "name":" name of the Category",
}
```
- Song API 

Med Postman eller Thunder Client man kan testa API och CRUD-funktionalitet med nedstående:

| Domain | Method    | URI                   | BESKRIVNING  |
| ------------|-----------|------------------------------|-------------------------------------------------| 
|        | GET       | api/Song                          | Hämta ut all data från tabellen.                |
|        | GET       | api/Song/ID                       | Hämta ut en rad från tabellen med ett givet id. |
|        | POST      | api/Song                          | Lägga till data till tabellen                   |
|        | PUT       | api/Song/ID                       | Uppdatera data för en rad med ett givet id.     |      
|        | DELETE    | api/Song/ID                       | Radera en rad i tabellen med ett givet id.      |
|        | GET       | api/Song/artist/artistID/songs    | Hämta låter som tillhör en artist               |
|        | GET       | api/Song/category/categorID/songs | Hämta låter som finns i en category             |


Song data skickas som JSON data till API i följande format:

```
  {
    "title": "name of the song",
    "length": 200,
    "artistName": "name of the artist",
    "categoryName": "name of the category"
  }

```
#### Alaa Zaza