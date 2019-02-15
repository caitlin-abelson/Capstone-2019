$(document).ready(function(){
	
	// get an array of rows
	var rows = $('tr');

	// start a new string with the header, adding a count column
	var newRows = "<tr><th>Count</th>" + rows[0].innerHTML + "</tr>";

	// loop backward to add remaining rows, with count column
	for(i = rows.length -1; i > 0; i--) {
		newRows += "<tr><td>" + i + "</td>" + rows[i].innerHTML + "</tr>";
	}

	// replace the table's content with the new rows
	$('table').html(newRows);

	// add the closing table, body and html tags
	$('tr:last').append("</table></body></html>");
	
	// style our add-on column of row numbers
	$("tr td:first-child").css({"backgroundColor":"#aaf",
								"color":"#550",
								"textAlign":"center"});
	$("tr th:first-child").css({"backgroundColor":"#005",
								"color":"#ffa"});
});