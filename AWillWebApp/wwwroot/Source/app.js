"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// TODO: find a way to avoid including
// jquery in app entry as follows
var jQuery = require("jquery");
window.$ = window.jQuery = jQuery;
var lib_1 = require("./lib");
$('#fillthiswithjquery').html('Filled by Jquery!');
document.getElementById('fillthis')
    .appendChild(document.createTextNode(lib_1.getText()));
//# sourceMappingURL=app.js.map