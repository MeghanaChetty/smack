BEGIN TRANSACTION;
BEGIN TRY

    PRINT 'Creating indexes for performance optimization...';

    -- ============================================
    -- UserRestaurants Indexes (for access control lookups)
    -- ============================================
    CREATE NONCLUSTERED INDEX IX_UserRestaurants_UserId 
        ON user_restaurants (user_id);
    
    CREATE NONCLUSTERED INDEX IX_UserRestaurants_RestaurantId 
        ON user_restaurants (restaurant_id);
    
    CREATE NONCLUSTERED INDEX IX_UserRestaurants_RoleId 
        ON user_restaurants (role_id);
    
    PRINT 'UserRestaurants indexes created.';

    -- ============================================
    -- MenuItems Index (for quickly fetching a restaurant's menu)
    -- ============================================
    CREATE NONCLUSTERED INDEX IX_MenuItems_RestaurantId 
        ON menuitems (restaurant_id);  -- Changed from menu_items
    
    PRINT 'MenuItems indexes created.';

    -- ============================================
    -- RestaurantTables Index (for quickly fetching tables belonging to a restaurant)
    -- ============================================
    CREATE NONCLUSTERED INDEX IX_RestaurantTables_RestaurantId 
        ON restauranttables (restaurant_id);  -- Changed from restaurant_tables
    
    PRINT 'RestaurantTables indexes created.';

    -- ============================================
    -- Orders Index (for quickly fetching orders belonging to a restaurant)
    -- ============================================
    CREATE NONCLUSTERED INDEX IX_Orders_RestaurantId 
        ON orders (restaurant_id);
    
    PRINT 'Orders indexes created.';

    -- ============================================
    -- QRCode Index (for quick table lookup when scanning QR)
    -- ============================================
    CREATE NONCLUSTERED INDEX IX_RestaurantTables_QRCode 
        ON restauranttables (QRCode);
    
    PRINT 'QRCode index created.';

    -- ============================================
    -- Orders Status and Live Filters (composite index for common queries)
    -- ============================================
    CREATE NONCLUSTERED INDEX IX_Orders_Status_IsLive 
        ON orders (status, islive);
    
    PRINT 'Orders status index created.';

    COMMIT TRANSACTION;
    PRINT '========================================';
    PRINT 'ALL INDEXES CREATED SUCCESSFULLY!';
    PRINT '========================================';

END TRY
BEGIN CATCH
    IF (XACT_STATE()) <> 0
    BEGIN
        ROLLBACK TRANSACTION;
        PRINT 'Index creation FAILED. Transaction rolled back.';
    END

    DECLARE @ErrorMessage NVARCHAR(MAX), @ErrorSeverity INT, @ErrorState INT;
    SELECT 
        @ErrorMessage = ERROR_MESSAGE(), 
        @ErrorSeverity = ERROR_SEVERITY(), 
        @ErrorState = ERROR_STATE();

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH