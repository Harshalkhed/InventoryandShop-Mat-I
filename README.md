
# <a name="_nwic6iwb3wnq"></a>**Inventory and Shop** 
![](Aspose.Words.6926c67c-617c-4f72-9df0-8ee818a99074.001.png)

One Panel represents Shop and the other represents Player Inventory

current **Currency** will displayed on the top of the Screen

Initially, your player inventory will be empty and you will have no money

There will be many items in the shop that you can buy.

Each item in the shop or inventory will have the following properties:

Type

Icon

Item description

Buying Price

Selling Price

Weight

Rarity

Quantity
### <a name="_6q8fdflsnnqn"></a>**Item Types**
-----
Items can be of the following types:

Materials

Weapons

Consumables

Treasure

The shop UI will have 4 tabs for each item type

Selecting a tab will display available items of that type in the shop.

### <a name="_fwtibvfzclrk"></a>**Selling & Buying Price**
-----
- When an item is in the shop, selecting that item should show it’s buying price
- When an item is in the inventory, selecting that should show it’s selling price
### <a name="_hrefgmz6bge7"></a>**Weight**
-----
- There would be a maximum weight the Player can carry in their inventory.
- Each item will have a certain weight.
- When an item is bought or sold the player’s inventory weight will be updated accordingly.
- The cumulative weight of all the items in the player inventory should always be less than the Maximum Weight.
### <a name="_jffl1veoe360"></a>**Rarity**
-----
Each item can be of any of the below Rarity Levels:

Very Common

Common

Rare

Epic

Legendary

The Value of the item will accordingly be more or less.
### <a name="_7jvnc770lx4o"></a>**Icon & Description**
-----
- Each item will have a unique Icon and Text Description
- Any of the Items in inventory or the shop when selected, its icon, description, value, weight, and any other details will be shown in UI like in the below reference image:

![](Aspose.Words.6926c67c-617c-4f72-9df0-8ee818a99074.002.png)
### <a name="_lc04bt6s7xer"></a>**Gathering Resources**
-----
- At the bottom, there should be a button to gather resources.

- If the inventory’s weight offshoots the maximum weight that can be carried, the gathering resources button will not work and a popup will be shown to indicate the same to the player.
### <a name="_5lcztbrg1nfv"></a>**Buying and Selling Items**
-----
- Each Item in the shop when selected will show a Buy Button while showing its details.
- If you have enough money you can buy that item and it will shift in your inventory.
- If you don’t have enough money, a popup should be displayed to indicate the same.
- If you select an item in your inventory there should be a sell button along with the details of that item being shown.
- Clicking it should remove that item from the inventory and move it back to the shop.
- Upon selecting “Buy Item” and “Sell Item” button, a popup will be shown to the player where they can select the quantity they want to buy or sell.
- Player can increase or decrease the quantity of the item using ‘+’ and ‘-’ buttons displayed in the popup:

![](Aspose.Words.6926c67c-617c-4f72-9df0-8ee818a99074.003.png)

- After selecting the quantity of the item, there should be a popup asking for conformation like shown in the example below:

![](Aspose.Words.6926c67c-617c-4f72-9df0-8ee818a99074.004.png)

When buying an item from the shop: (If conditions are satisfied)

Resources will be deducted

An overlay text will appear for a few seconds 

The bought item will be placed in your inventory UI

Current weight of the inventory will be increased accordingly.


When Selling an item:

Resources will be increased

Inventory UI will be updated

An overlay text will appear for a few seconds 

The current Cumulative weight of the inventory will be decreased


- Use of Design Patterns 
- MVC
- DependencyInjection
- Observer


