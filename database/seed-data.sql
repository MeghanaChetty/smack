BEGIN TRANSACTION;
BEGIN TRY

    -- ============================================
    -- STEP 1: Insert Roles
    -- ============================================
    INSERT INTO roles (rolename, description)
    VALUES 
        ('Owner', 'Full access to all restaurant features'),
        ('Manager', 'Can manage menu items and orders'),
        ('Chef', 'Can view and update order status only'),
        ('Staff', 'Can view orders only'),
        ('Guest', 'Customer who orders food');

    PRINT 'Roles inserted successfully.';

    -- ============================================
    -- STEP 2: Insert Order Status
    -- ============================================
    INSERT INTO orderstatus (statusname, description)
    VALUES 
        ('Pending', 'Order placed, waiting to be prepared'),
        ('Preparing', 'Kitchen is preparing the order'),
        ('Delivered', 'Order delivered to table'),
        ('Completed', 'Order completed and paid'),
        ('Cancelled', 'Order was cancelled');

    PRINT 'Order statuses inserted successfully.';

    -- ============================================
    -- STEP 3: Insert Categories
    -- ============================================
    INSERT INTO categories (categoryname, description)
    VALUES 
        ('Appetizer', 'Starters and small dishes'),
        ('Main Course', 'Main dishes and entrees'),
        ('Sides', 'Side dishes and accompaniments'),
        ('Beverage', 'Drinks and beverages'),
        ('Dessert', 'Sweet dishes and desserts');

    PRINT 'Categories inserted successfully.';

    -- ============================================
    -- STEP 4: Insert Restaurants
    -- ============================================
    INSERT INTO restaurants (restaurant_name, email, phone_number, address)
    VALUES 
        ('Pizza Palace', 'contact@pizzapalace.com', '555-0101', '123 Main St, Food City'),
        ('Burger Bay', 'info@burgerbay.com', '555-0102', '456 Oak Ave, Food City');

    PRINT 'Restaurants inserted successfully.';

    -- ============================================
    -- STEP 5: Insert Users
    -- ============================================
    -- Get role IDs for reference
    DECLARE @OwnerRoleId INT = (SELECT role_id FROM roles WHERE rolename = 'Owner');
    DECLARE @ManagerRoleId INT = (SELECT role_id FROM roles WHERE rolename = 'Manager');
    DECLARE @ChefRoleId INT = (SELECT role_id FROM roles WHERE rolename = 'Chef');
    DECLARE @GuestRoleId INT = (SELECT role_id FROM roles WHERE rolename = 'Guest');

    INSERT INTO users (email, username, usertype)
    VALUES 
        ('john@foodgroup.com', 'John Doe', @OwnerRoleId),           -- Multi-restaurant owner
        ('sarah@pizzapalace.com', 'Sarah Manager', @ManagerRoleId), -- Pizza Palace manager
        ('mike@burgerbay.com', 'Mike Chef', @ChefRoleId),          -- Burger Bay chef
        ('guest@example.com', 'Guest Customer', @GuestRoleId);     -- Guest user

    PRINT 'Users inserted successfully.';

    -- ============================================
    -- STEP 6: Link Users to Restaurants (UserRestaurants)
    -- ============================================
    -- Get user IDs
    DECLARE @JohnUserId INT = (SELECT user_id FROM users WHERE email = 'john@foodgroup.com');
    DECLARE @SarahUserId INT = (SELECT user_id FROM users WHERE email = 'sarah@pizzapalace.com');
    DECLARE @MikeUserId INT = (SELECT user_id FROM users WHERE email = 'mike@burgerbay.com');

    -- Get restaurant IDs
    DECLARE @PizzaPalaceId INT = (SELECT restaurant_id FROM restaurants WHERE restaurant_name = 'Pizza Palace');
    DECLARE @BurgerBayId INT = (SELECT restaurant_id FROM restaurants WHERE restaurant_name = 'Burger Bay');

    INSERT INTO user_restaurants (user_id, restaurant_id, role_id)
    VALUES 
        -- John is Owner of both restaurants
        (@JohnUserId, @PizzaPalaceId, @OwnerRoleId),
        (@JohnUserId, @BurgerBayId, @OwnerRoleId),
        
        -- Sarah is Manager of Pizza Palace
        (@SarahUserId, @PizzaPalaceId, @ManagerRoleId),
        
        -- Mike is Chef at Burger Bay
        (@MikeUserId, @BurgerBayId, @ChefRoleId);
        
    -- Note: Guest user has NO entries in user_restaurants

    PRINT 'User-Restaurant links created successfully.';

    -- ============================================
    -- STEP 7: Insert Restaurant Tables
    -- ============================================
    INSERT INTO restauranttables (restaurant_id, table_number, QRCode)
    VALUES 
        -- Pizza Palace tables
        (@PizzaPalaceId, 1, 'QR-PIZZA-T1'),
        (@PizzaPalaceId, 2, 'QR-PIZZA-T2'),
        (@PizzaPalaceId, 3, 'QR-PIZZA-T3'),
        
        -- Burger Bay tables
        (@BurgerBayId, 1, 'QR-BURGER-T1'),
        (@BurgerBayId, 2, 'QR-BURGER-T2');

    PRINT 'Restaurant tables inserted successfully.';

    -- ============================================
    -- STEP 8: Insert Menu Items
    -- ============================================
    -- Get category IDs
    DECLARE @AppetizerCatId INT = (SELECT id FROM categories WHERE categoryname = 'Appetizer');
    DECLARE @MainCourseCatId INT = (SELECT id FROM categories WHERE categoryname = 'Main Course');
    DECLARE @SidesCatId INT = (SELECT id FROM categories WHERE categoryname = 'Sides');
    DECLARE @BeverageCatId INT = (SELECT id FROM categories WHERE categoryname = 'Beverage');

    -- Pizza Palace Menu (CreatedBy = John)
    INSERT INTO menuitems (restaurant_id, itemname, description, price, category, createdby)
    VALUES 
        (@PizzaPalaceId, 'Margherita Pizza', 'Classic cheese and tomato pizza', 12.99, @MainCourseCatId, @JohnUserId),
        (@PizzaPalaceId, 'Pepperoni Pizza', 'Pizza with pepperoni and cheese', 14.99, @MainCourseCatId, @JohnUserId),
        (@PizzaPalaceId, 'Caesar Salad', 'Fresh romaine with caesar dressing', 8.99, @AppetizerCatId, @JohnUserId),
        (@PizzaPalaceId, 'Garlic Bread', 'Toasted bread with garlic butter', 5.99, @AppetizerCatId, @JohnUserId),
        (@PizzaPalaceId, 'Coke', 'Chilled soft drink', 2.99, @BeverageCatId, @JohnUserId);

    -- Burger Bay Menu (CreatedBy = John)
    INSERT INTO menuitems (restaurant_id, itemname, description, price, category, createdby)
    VALUES 
        (@BurgerBayId, 'Classic Burger', 'Beef burger with lettuce and tomato', 10.99, @MainCourseCatId, @JohnUserId),
        (@BurgerBayId, 'Cheese Burger', 'Burger with extra cheese', 11.99, @MainCourseCatId, @JohnUserId),
        (@BurgerBayId, 'French Fries', 'Crispy golden fries', 4.99, @SidesCatId, @JohnUserId),
        (@BurgerBayId, 'Milkshake', 'Creamy chocolate milkshake', 5.99, @BeverageCatId, @JohnUserId);

    PRINT 'Menu items inserted successfully.';

    -- All inserts successful
    COMMIT TRANSACTION;
    PRINT '========================================';
    PRINT 'ALL SAMPLE DATA INSERTED SUCCESSFULLY!';
    PRINT '========================================';

END TRY
BEGIN CATCH
    -- Rollback on error
    IF (XACT_STATE()) <> 0
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT 'Data insertion FAILED. Transaction rolled back.';
    END

    -- Display error
    DECLARE @ErrorMessage NVARCHAR(MAX), @ErrorSeverity INT, @ErrorState INT;
    SELECT 
        @ErrorMessage = ERROR_MESSAGE(), 
        @ErrorSeverity = ERROR_SEVERITY(), 
        @ErrorState = ERROR_STATE();

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH