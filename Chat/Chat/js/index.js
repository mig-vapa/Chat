var vmChat = new Vue({
	el: '#app',
	data: {
		messages: []
	}
});

var results;
var incoming;
var valido;
function successReadMessages(a) {
	//console.log('salve')
	incoming = a;
	//console.log('method');
	try {
		//console.log('try');
		//console.log(a);
		if (a.indexOf("error") != -1) {
			console.log(a);
		}
		if (a != 'vazio') {
			valido = a;
			results = JSON.parse(a.replace(/'/g, '"'));
			vmChat.messages = JSON.parse(a.replace(/'/g, '"'));
			for (i = 0; i < results.length; i++) console.log(results[i]);
			console.log(results);
			vmChat.messages = results;
		}
		//console.log('end-try');
	}
	catch (aa) {
		//console.log('catch');
	}
	finally {
		//console.log('finally');
		Chat.ReadMessages(successReadMessages);
	}
}

function successSendMessage(a) {
	console.log(a);
	try {
		if (a) {
			results = JSON.parse(a.replace(/'/g, '"'));
			console.log(results);
		}
	}
	catch{ }
}