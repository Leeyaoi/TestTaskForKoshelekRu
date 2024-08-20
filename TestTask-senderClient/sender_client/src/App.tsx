import {
  HubConnectionBuilder,
  LogLevel,
  HubConnection,
  HttpTransportType,
} from "@microsoft/signalr";
import * as React from "react";
import TextField from "@mui/material/TextField";
import { Button, Container } from "@mui/material";
import client from "./client.ts";

function App() {
  const [text, setText] = React.useState("");
  const [number, setNumber] = React.useState(0);
  const [connection, setConnection] = React.useState<HubConnection>(
    {} as HubConnection
  );

  const join = async () => {
    const connection = new HubConnectionBuilder()
      .withUrl("https://localhost:7133/api/Messages/hub", {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();
    try {
      await connection.start();
      console.log(connection);

      setConnection(connection);
    } catch (e) {
      console.log("error: " + e);
    }
  };

  React.useEffect(() => {
    if (!("state" in connection)) {
      join();
    }
  }, [connection]);

  const send = async () => {
    try {
      const res = await client.post("", { text: text, number: number });
      setNumber(number + 1);
      console.log(res.data);
      connection.invoke("send", res.data);
      setText("");
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <Container sx={{ marginTop: "0.5rem" }}>
      <TextField
        multiline
        maxRows={4}
        sx={{ width: "100%" }}
        label="Write a message"
        value={text}
        onChange={(newValue) => setText(newValue.target.value.slice(0, 128))}
      />
      <br></br>
      <Button variant="contained" onClick={send}>
        Send
      </Button>
    </Container>
  );
}

export default App;
