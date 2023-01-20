# ConsoleScreenHandler_EK(MV)

Second version of the POC of a ScreenHandler api, which creates console menus based on a json given to it by the user.

Now the handler can manage CRUD operations on a new listing created for the apps main screen.

MainScreen structure
```json
{
 {
  "ID": "StartingScreen",
  "EntryPoint": true,
  "Title": "Bienvenido!",
  "Listing": [
  {
   "ListingCode": "adm315",
   "ListingMessage": "El codigo de los listings pueden ser letras y numeros, siempre en minuscula."
  }
  ],
  "Message": null,
  "Fields": [
  {
   "Code": "N",
   "Desc": "Crea una nueva entrada."
  },
  {
   "Code": "M",
   "Desc": "Modifica una entrada existente."
  },
  {
   "Code": "F",
   "Desc": "Borra una entrada existente."
  },
  {
   "Code": "A",
   "Desc": "Abre una pagina con un mensaje."
  },
  {
   "Code": "C",
   "Desc": "Sal de la aplicacion."
  }
  ]
  "Actions": [
  {
   "Name": "CrearEntrada",
   "Option": "n",
   "Handler": "create",
   "NextScreen": "StartingScreen"
  },
  {
   "Name": "ActualizarEntrada",
   "Option": "m",
   "Handler": "update",
   "NextScreen": "StartingScreen"
  },
  {
   "Name": "BorrarEntrada",
   "Option": "f",
   "Handler": "delete",
   "NextScreen": "StartingScreen"
  },
  {
   "Name": "NextPage",
   "Option": "a",
   "Handler": "NextScreen",
   "NextScreen": "FollowUpScreen"
  },
  {
   "Name": "Salir",
   "Option": "c",
   "Handler": "exit",
   "NextScreen": "StartingScreen"
  }
  ]
 }
}
```

EntryPoint
> There should only be a single entry point between all screens, if there are multiple, the app will choose the first one it finds to display.

Listing
> List displayed on the EntryPoint screen, can be managed via CRUD operations by creating the appropiate actions

Message and Field
> A screen cannot have both message and fields, as they both serve to display a message in different ways, if both are present, the message will take priority, and fields will not be displayed.

Actions
> Both the Option property and the Handler property are case insensitive, but the NextScreen property is, so be careful.
