const client = axios.create({
  baseURL: "https://localhost:7133/api/Messages",
  timeout: 1000,
  headers: { "X-Custom-Header": "foobar" },
});

document.getElementById("SendMessageForm").onload = sessionStorage.setItem(
  "number",
  0
);

document.getElementById("SendMessageForm").addEventListener("submit", submit);

async function submit() {
  event.preventDefault();
  if (sessionStorage.getItem("number")) {
    const num = sessionStorage.getItem("number");
    await client.post("", {
      text: document.getElementById("MessageInput").value,
      number: num,
    });
    sessionStorage.setItem("number", parseInt(num) + 1);
  } else {
    sessionStorage.setItem("number", 0);
  }
}
