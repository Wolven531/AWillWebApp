// TODO: find a way to avoid including
// jquery in app entry as follows
import * as jQuery from 'jquery'
(window as any).$ = (window as any).jQuery = jQuery

import { getText } from './lib'

$('#fillthiswithjquery').html('Filled by Jquery!')

document.getElementById('fillthis')
	.appendChild(document.createTextNode(getText()))
