<?php
$servername = "localhost"; // replace with your server name
$username = "loot"; // replace with your username
$password = ""; // replace with your password
$dbname = "testv1"; // replace with your database name

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

// Extract the data from the POST request
$temperature = $_POST["Temperature"];
$humidity = $_POST["Humidity"];
$ammonium = $_POST["Ammonium"];
$co2 = $_POST["CO2"];

// Prepare the SQL query
$sql = "INSERT INTO sensordata (Timestamp, Temperature, Humidity, Ammonium, CO2) VALUES (NOW(), ?, ?, ?, ?)";

// Prepare the statement
if ($stmt = $conn->prepare($sql)) {
    $stmt->bind_param('dddd', $temperature, $humidity, $ammonium, $co2);
    
    // Execute the query
    if ($stmt->execute()) {
        echo "New record created successfully";
    } else {
        echo "Error: " . $sql . "<br>" . $conn->error;
    }
    
    $stmt->close();
}

$conn->close();
?>
