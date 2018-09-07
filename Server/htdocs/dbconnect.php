<?php
$MyUsername = "root";  // enter your username for mysql
$MyPassword = "password";  // enter your password for mysql
$MyHostname = "localhost";      // this is usually "localhost" unless your database resides on a different server
$dbh =	"rfid";
$con = mysqli_connect($MyHostname,$MyUsername, $MyPassword, $dbh);

// Check connection
if (mysqli_connect_errno())
  {
  echo "Failed to connect to MySQL: " . mysqli_connect_error();
  }
?>