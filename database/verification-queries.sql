-- Query 1: Count all records
SELECT 'Roles' AS TableName, COUNT(*) AS RecordCount FROM roles
UNION ALL
SELECT 'Order Statuses', COUNT(*) FROM orderstatus
UNION ALL
SELECT 'Categories', COUNT(*) FROM categories
UNION ALL
SELECT 'Restaurants', COUNT(*) FROM restaurants
UNION ALL
SELECT 'Users', COUNT(*) FROM users
UNION ALL
SELECT 'User-Restaurant Links', COUNT(*) FROM user_restaurants
UNION ALL
SELECT 'Restaurant Tables', COUNT(*) FROM restauranttables
UNION ALL
SELECT 'Menu Items', COUNT(*) FROM menuitems;

-- Query 2: Show John's restaurant access
SELECT 
    u.username,
    r.restaurant_name,
    ro.rolename
FROM users u
JOIN user_restaurants ur ON u.user_id = ur.user_id
JOIN restaurants r ON ur.restaurant_id = r.restaurant_id
JOIN roles ro ON ur.role_id = ro.role_id
WHERE u.email = 'john@foodgroup.com';

-- Query 3: Show all users with their restaurant access
SELECT 
    u.username,
    u.email,
    r.restaurant_name,
    ro.rolename AS role_in_restaurant
FROM users u
LEFT JOIN user_restaurants ur ON u.user_id = ur.user_id
LEFT JOIN restaurants r ON ur.restaurant_id = r.restaurant_id
LEFT JOIN roles ro ON ur.role_id = ro.role_id
ORDER BY u.username, r.restaurant_name;

-- Query 4: Menu items by restaurant
SELECT 
    r.restaurant_name,
    c.categoryname,
    m.itemname,
    m.price
FROM menuitems m
JOIN restaurants r ON m.restaurant_id = r.restaurant_id
JOIN categories c ON m.category = c.id
ORDER BY r.restaurant_name, c.categoryname, m.itemname;