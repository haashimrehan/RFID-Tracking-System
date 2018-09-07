<?php
    // Connect to MySQL
    include 'dbconnect.php';
	
	$hasBeenThere = false;
	$differentBench = false;
	$atBench;
	$SQL = "INSERT INTO `log` (`location` , `uid`) VALUES ('".$_GET["location"]."', '".$_GET["uid"]."')";
	
	$location2 = $_GET["location"];
	$uid2 = $_GET["uid"];
	
	$query  = "SELECT * FROM log WHERE location like '%".$location2."%' OR uid like '%".$uid2."%' ORDER BY event desc";

	$unique_array = array(
    'location'=>array(),
    'uid'=>array(),
    );
	$queryResult = mysqli_query($con, $query);
	while($row = mysqli_fetch_array($queryResult))
{
$unique_array[] = array('location'=>$row['location'], 'uid'=>$row['uid']);
if ($row['location'] === $location2 && $row['uid'] === $uid2) {
	if ($differentBench === false) {
	$hasBeenThere = true;
	$atBench = $row['location'];
 }
}
if ($row['location'] !== $location2 && $row['uid'] === $uid2) {
	if ($hasBeenThere === false) {
	$differentBench = true;
	$atBench = $row['location'];
	}
} 

echo $row['location']." ";
echo $row['uid']." ";

}

echo 'hasBeenThere ='.$hasBeenThere;
echo 'At Bench = '.$differentBench.$atBench;

    // Execute SQL statement
  if ($hasBeenThere === true && $atBench === $location2) {
  } else {
	mysqli_query($con, $SQL);    
  }
  ?>