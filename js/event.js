var event = new Event('build');
var elem = document.querySelector('#id')
  
// Listen for the event.
elem.addEventListener('build', function (e) { console.log(e) }, false);
  
// Dispatch the event.
elem.dispatchEvent(event);