import React, { useState } from "react";
import {
  HubConnectionBuilder,
  HttpTransportType,
  HubConnection,
  LogLevel,
} from "@microsoft/signalr";
import MessageType from "./MessageType";
import { Container } from "@mui/material";
import DataTable from "./DataTable.tsx";

function App() {
  const [rows, setRows] = useState<MessageType[]>([]);
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
    connection.on("Recieve", (message: MessageType) => {
      setRows((prevRows) => {
        if (
          !prevRows.some(
            (row) => row.number === message.number && row.text === message.text
          )
        ) {
          return [...prevRows, message];
        }
        return prevRows;
      });
    });
    try {
      await connection.start();

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

  return (
    <Container>
      <DataTable rows={rows} />
    </Container>
  );
}

export default App;
