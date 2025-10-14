BEGIN TRANSACTION;
BEGIN TRY
			--Table 1: Restaurants
			--The foundation - every restaurant on your platform.
			--Columns you need:

			--Primary key (auto-increment integer)
			--RestaurantName (required, max 200 chars)
			--Email (required, unique, max 255 chars)
			--PhoneNumber (optional, max 20 chars)
			--Address (optional, max 500 chars)
			--CreatedAt (timestamp, defaults to current time)
			--IsActive (bit/boolean, defaults to true/1)

			CREATE TABLE restaurants (
				restaurant_id INT PRIMARY KEY IDENTITY(1000,1),
				restaurant_name NVARCHAR(200) NOT NULL ,
				email NVARCHAR(255) NOT NULL UNIQUE,
				phone_number NVARCHAR(20) NULL,
				address NVARCHAR(500) NULL,
				created_at DATETIME2 DEFAULT GETDATE(),
				is_active BIT DEFAULT 1
			);


			--Table 3: Roles ⭐ NEW - Lookup Table
			--Defines all possible roles in the system.
			--Columns you need:

			--Primary key (auto-increment integer)
			--RoleName (required, unique, max 50 chars)
			--Description (optional, max 200 chars)
			--CreatedAt (timestamp, defaults to current time)

			--This is a lookup/reference table - pre-populate with standard roles.

			CREATE TABLE roles (

				role_id INT PRIMARY KEY IDENTITY(1,1),
				rolename NVARCHAR(100) NOT NULL,
				description NVARCHAR(500) NULL,
				created_at DATETIME2 DEFAULT GETDATE()
				);

			--Table 2: Users
			--All users - both restaurant admins AND guests.
			--Columns you need:

			--Primary key (auto-increment integer)
			--Email (required, unique, max 255 chars)
			--GoogleId (optional, max 255 chars - for OAuth later)
			--UserName (required, max 100 chars)
			--UserType (required, only allows 'Guest' or 'RestaurantAdmin')
			--CreatedAt (timestamp, defaults to current time)
			--IsActive (bit/boolean, defaults to true/1)

			--Note: No RestaurantId column here! Users link to restaurants through the junction table.


			CREATE TABLE users (
				user_id INT PRIMARY KEY IDENTITY(10000,1),
				email NVARCHAR(200) NOT NULL UNIQUE,
				google_id NVARCHAR(200) NULL,
				username NVARCHAR(100) NOT NULL,
				usertype INT NOT NULL,
				created_at DATETIME2 DEFAULT GETDATE(),
				is_active BIT DEFAULT 1,

				CONSTRAINT FK_User_Roles
						FOREIGN KEY (usertype) REFERENCES roles(role_id)
						ON DELETE NO ACTION,
			);





			--Table 4: UserRestaurants - Junction Table
			--Maps which users have access to which restaurants with what role.
			--Columns you need:

			--Primary key (auto-increment integer)
			--UserId (required integer)
			--RestaurantId (required integer)
			--RoleId (required integer) ⭐ Foreign key to Roles table
			--CreatedAt (timestamp, defaults to current time)

			--Foreign Keys:

			--UserId links to Users (ON DELETE CASCADE)
			--RestaurantId links to Restaurants (ON DELETE CASCADE)
			--RoleId links to Roles (ON DELETE NO ACTION - can't delete a role if it's in use)

			--Unique Constraint:

			--Combination of (UserId, RestaurantId) must be unique
			--One user can't have multiple roles in the same restaurant (keep it simple for now)

			CREATE TABLE user_restaurants (
					id INT PRIMARY KEY IDENTITY(1,1),
					user_id INT NOT NULL,
					restaurant_id INT NOT NULL,
					role_id INT NOT NULL,
					created_at DATETIME2 DEFAULT GETDATE(),

					CONSTRAINT FK_UserRestaurant_users
						FOREIGN KEY (user_id) REFERENCES users(user_id)
						ON DELETE CASCADE,
					CONSTRAINT FK_UserRestaurant_Restaurantid
						FOREIGN KEY (restaurant_id) references restaurants(restaurant_id)
						ON DELETE CASCADE,
					CONSTRAINT FK_UserRestaurants_Roles
						FOREIGN KEY (role_Id) REFERENCES roles(role_id)
						ON DELETE NO ACTION,

					CONSTRAINT UQ_Userrestaurant
						UNIQUE (user_id,restaurant_id)


			);
			--categorytable i created this because i need different tabs
			CREATE TABLE categories (

				id INT PRIMARY KEY IDENTITY(1,1),
				categoryname NVARCHAR(100) NOT NULL,
				description NVARCHAR(500) NULL,
				created_at DATETIME2 DEFAULT GETDATE()
				);

			--Table 5: MenuItems
			--Each restaurant's menu.
			--Columns you need:

			--Primary key (auto-increment integer)
			--RestaurantId (required integer)
			--ItemName (required, max 200 chars)
			--Description (optional, max 500 chars)
			--Price (required, decimal with 10 digits, 2 after decimal point)
			--Category (optional, max 100 chars)
			--IsAvailable (bit/boolean, defaults to true/1)
			--CreatedAt (timestamp, defaults to current time)
			--CreatedBy (required integer - which admin added this)

			--Foreign Keys:

			--RestaurantId links to Restaurants (ON DELETE CASCADE)
			--CreatedBy links to Users table

			CREATE TABLE menuitems(
				id INT PRIMARY KEY IDENTITY(1,1),
				restaurant_id INT NOT NULL,
				itemname NVARCHAR(100) NOT NULL,
				description NVARCHAR(500) NULL,
				price DECIMAL(10,2) NOT NULL,
				category INT NOT NULL,
				isavailable BIT DEFAULT 1,
				created_at DATETIME2 DEFAULT GETDATE(),
				createdby int null,


				CONSTRAINT FK_menuitems_restaurants
						FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id)
						ON DELETE CASCADE,
				CONSTRAINT FK_Menuitems_users
						FOREIGN KEY (createdby) REFERENCES users(user_id)
						ON DELETE NO ACTION,
				CONSTRAINT FK_menuitems_categories
						FOREIGN KEY (category) REFERENCES categories(id)
						ON DELETE NO ACTION,


			);

			--Table 6: RestaurantTables
			--Physical tables in each restaurant.
			--Columns you need:

			--Primary key (auto-increment integer)
			--RestaurantId (required integer)
			--TableNumber (required, max 20 chars)
			--QRCode (optional, max 500 chars - unique across all restaurants)
			--IsOccupied (bit/boolean, defaults to false/0)
			--CreatedAt (timestamp, defaults to current time)

			--Foreign Key:

			--RestaurantId links to Restaurants (ON DELETE CASCADE)

			--Unique Constraints:

			--Combination of (RestaurantId, TableNumber) must be unique
			--QRCode must be unique across entire platform


			CREATE TABLE restauranttables(
				table_id INT PRIMARY KEY IDENTITY(1,1),
				restaurant_id INT NOT NULL,
				table_number INT NOT NULL,
				QRCode NVARCHAR(100) UNIQUE NULL,
				isoccupied BIT DEFAULT 0,
				created_at DATETIME2 DEFAULT GETDATE(),


				CONSTRAINT FK_restauranttables_restaurants
						FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id)
						ON DELETE CASCADE,
				CONSTRAINT UQ_restauranttable
						UNIQUE (restaurant_id,table_number)
			);


			--statustable - created so we can maintain all the status in one table
			CREATE TABLE orderstatus (

				id INT PRIMARY KEY IDENTITY(1,1),
				statusname NVARCHAR(100) NOT NULL,
				description NVARCHAR(500) NULL,
				created_at DATETIME2 DEFAULT GETDATE()
				);


			--Table 7: Orders
			--Customer orders.
			--Columns you need:

			--Primary key (auto-increment integer)
			--RestaurantId (required integer)
			--TableId (required integer)
			--UserId (required integer - the guest who ordered)
			--OrderDate (timestamp, defaults to current time)
			--TotalAmount (decimal 10,2, defaults to 0)
			--Status (required, only allows: 'Pending', 'Preparing', 'Delivered', 'Completed', 'Cancelled', defaults to 'Pending')
			--IsLive (bit/boolean, defaults to true/1)

			--Foreign Keys:

			--RestaurantId links to Restaurants
			--TableId links to RestaurantTables
			--UserId links to Users
			CREATE TABLE orders(
				order_id INT PRIMARY KEY IDENTITY(1,1),
				restaurant_id INT NOT NULL,
				table_id INT NOT NULL,
				user_id INT NOT NULL,
				orderdate DATETIME2 DEFAULT GETDATE(),
				totalamount DECIMAL(10,2) NOT NULL DEFAULT 0,
				status INT NOT NULL default 1,
				islive BIT DEFAULT 1,

				CONSTRAINT FK_orders_restaurants
						FOREIGN KEY (restaurant_id) REFERENCES restaurants(restaurant_id)
						ON DELETE CASCADE,
				CONSTRAINT FK_orders_tables
						FOREIGN KEY (table_id) REFERENCES restauranttables(table_id)
						ON DELETE NO ACTION,
	
				CONSTRAINT FK_order_users
						FOREIGN KEY (user_id) REFERENCES users(user_id)
						ON DELETE CASCADE,

				CONSTRAINT FK_orders_status
						FOREIGN KEY (status) REFERENCES orderstatus(id)
						ON DELETE NO ACTION
			);


			--Table 8: OrderItems
			--Individual items within each order.
			--Columns you need:

			--Primary key (auto-increment integer)
			--OrderId (required integer)
			--MenuItemId (required integer)
			--Quantity (required integer, defaults to 1)
			--Price (required decimal 10,2 - captured at time of order)
			--Subtotal (computed column: Quantity × Price, persisted)

			--Foreign Keys:

			--OrderId links to Orders (ON DELETE CASCADE)
			--MenuItemId links to MenuItems
			CREATE TABLE orderitems(
				id INT PRIMARY KEY IDENTITY(1,1),
				order_id INT NOT NULL,
				menu_item_id INT NOT NULL,
				quantity INT NOT NULL DEFAULT 1,
				price DECIMAL(10,2) NOT NULL,
				Subtotal AS (quantity * price) PERSISTED,    

				CONSTRAINT FK_OrderItems_Orders
					FOREIGN KEY (Order_Id) REFERENCES Orders(Order_Id)
					ON DELETE CASCADE,                          

				CONSTRAINT FK_OrderItems_MenuItems
					FOREIGN KEY (menu_item_id) REFERENCES MenuItems(id)
					ON DELETE NO ACTION     
			);
    -- If we reach this point, all CREATE TABLE statements were successful.
    COMMIT TRANSACTION;
    PRINT 'Schema creation completed successfully. All tables committed.';

END TRY
BEGIN CATCH

    -- Check if there is an active transaction that needs to be rolled back.
    IF (XACT_STATE()) <> 0
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT 'Schema creation FAILED.';
    END

    -- Compatible error raising for SQL Server versions < 2012
    DECLARE @ErrorMessage NVARCHAR(MAX), @ErrorSeverity INT, @ErrorState INT;
    SELECT 
        @ErrorMessage = ERROR_MESSAGE(), 
        @ErrorSeverity = ERROR_SEVERITY(), 
        @ErrorState = ERROR_STATE();

    -- Raise the error again.
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);

END CATCH

