<?php
include 'dbconnect.php';

// Start MySQL Connection

//$con = mysqli_connect($MyHostname,$MyUsername, $MyPassword, $dbh);

if (isset($_POST['search'])) {
    $valueToSearch = $_POST['valueToSearch'];
    // search in all table columns
    // using concat mysql function
    
    $query         = "SELECT * FROM `log` WHERE CONCAT(`id`, `event`, `location`, `uid`) LIKE '%" . $valueToSearch . "%' ORDER BY event desc";
    $search_result = filterTable($query);
    
} else {
    $query         = "SELECT * FROM log ORDER BY event desc";
    $search_result = filterTable($query);
}

// function to connect and execute the query
function filterTable($query)
{
	global $con;
    $filter_Result = mysqli_query($con, $query);
    return $filter_Result;
}
//<meta http-equiv="refresh" content="1">
?>

<html>
<head>
<meta http-equiv="refresh" content="1">
<title>RFID Log</title>
<style type="text/css">
.table_titles, .table_cells_odd, .table_cells_even {
    padding-right: 20px;
    padding-left: 20px;
color: #000;
}
.table_titles {
color: #FFF;
    background-color: #666;
}
.table_cells_odd {
    background-color: #CCC;
}
.table_cells_even {
    background-color: #FAFAFA;
}
table {
border: 2px solid #333;
}
body { font-family: "Trebuchet MS", Arial; }
</style>
</head>

<body>
<h1>Arduino location Log</h1>

<form action="review_data.php" method="post">
<input type="text" name="valueToSearch" placeholder="Value To Search">
<input type="submit" name="search" value="Filter"><br><br>

<table border="0" cellspacing="0" cellpadding="4">
<tr>
<td class="table_titles">ID</td>
<td class="table_titles">Date and Time</td>
<td class="table_titles">location</td>
<td class="table_titles">uid</td>
</tr>


<?php
$oddrow = true;
while ($row = mysqli_fetch_array($search_result)):
    if ($oddrow) {
        $css_class = ' class="table_cells_odd"';
    } else {
        $css_class = ' class="table_cells_even"';
    }
    $oddrow = !$oddrow;
    
    echo '<tr>';
    echo '   <td' . $css_class . '>' . $row["id"] . '</td>';
    echo '   <td' . $css_class . '>' . $row["event"] . '</td>';
    echo '   <td' . $css_class . '>' . $row["location"] . '</td>';
    echo '   <td' . $css_class . '>' . $row["uid"] . '</td>';
    echo '</tr>';
    
endwhile;
mysqli_close($con);
?>

</table>
</body>
</html>
