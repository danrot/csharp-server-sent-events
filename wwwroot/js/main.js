const $randomNumber = document.getElementById('random-number');
const $creationDate = document.getElementById('creation-date');

const eventSource = new EventSource('/Random');

eventSource.addEventListener('random', (event) => {
    const data = JSON.parse(event.data);
    console.log(data);
    $randomNumber.innerHTML = data.random;
    $creationDate.innerHTML = data.created;
});
