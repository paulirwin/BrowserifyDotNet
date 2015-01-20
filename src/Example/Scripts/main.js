var unique = require('uniq');
var jQuery = require('jquery');

var data = [1, 2, 2, 3, 4, 5, 5, 5, 6];

jQuery(function () {
    alert("jQuery is loaded by browserify!");
});

console.log(unique(data));