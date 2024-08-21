
# E-AuctionOnline web (not done)
## Description:
-this projet is smaller version of AuctionOnline project and use different appoarch , instead of using specification design patter E_AuctionOnline use chain of responsibilty desing pattern ,also change from nextjs to angular. Because the project still not done yet , this is what it have so far until now:
.
## Features:
- Admin Role:
    + View list of users and manage user accounts (lock/unlock).
    + Manage categories: view list, view category details, update category, and add/remove items to/from categories.
    + View list of items, item details, and manage items (add/remove categories).
- User Role:
    + Authentication: login, signup, reset password.
    + Security: JWT and refresh tokens for secure authentication.
    + Profile management: view and update profile details.
    + Selling: list items for auction, update item details.
    + Bidding: place bids on items, view auction history.

## Tech Stack:

- .NET Core (C#)
- Onion Architecture
- Specification Pattern
- JWT Authentication
- SignalR
- SQL Server
- Cloudinary
- CSS
-Angular
## Installation
1. Clone from git:
    https://github.com/hzost09/E_AuctionOnline.git
2. Set up environment variables in dotnet : 
    - Rename appsettings.sample.json in AutionOnline directory to appsettings.json and fill in these fields: 
        + CloudName
        + ApiKey
        + ApiSecret
        + Mail
        + Password
3. Run .NET server :    
 - Open .NET CLI and run command dotnet ef database update 
4.Run Angular server:
- Open terminal of VS code and run command ng server (since it just simple basic Angular, we don’t need install anything more )

