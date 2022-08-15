console.log('Hello World');
// We'll be using the https://github.com/theturtle32/WebSocket-Node
// WebSocket implementation
var WebSocketServer = require('websocket').server;
var http = require('http');

var server = http.createServer(function(request, response) {
  // process HTTP request. 
});
server.listen(1337, function() 
{
	 console.log("trying to listen on 1337");
	});

// create the server
wsServer = new WebSocketServer(
{
  httpServer: server

}
);

// WebSocket server
wsServer.on('request', function(request) {
  var connection = request.accept(null, request.origin);
	console.log('websocket on request')
  // This is the most important callback for us, we'll handle
  // all messages from users here.
  connection.on('message', function(message){
      console.log('websocket on message... '+ message);
      console.log(message);
      //connection.sendUTF("We got: " + message.utf8Data);
  });

  connection.on('close', function(connection) {
		console.log('websocket on close...')
		
  });
});