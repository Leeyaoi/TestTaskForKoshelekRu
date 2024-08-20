import "./App.css";
import { useState } from "react";
import dayjs, { Dayjs } from "dayjs";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DateTimePicker } from "@mui/x-date-pickers/DateTimePicker";
import { Button, Container } from "@mui/material";
import client from "./api/client";
import React from "react";
import DataTable from "./DataTable.tsx";

function App() {
  const [startDateTime, setStartDateTime] = useState<Dayjs | null>(dayjs());
  const [endDateTime, setEndDateTime] = useState<Dayjs | null>(dayjs());

  const [rows, setRows] = useState<any[] | null>(null);

  const getMessages = async () => {
    try {
      const res = await client.get(
        `?start=${startDateTime}&end=${endDateTime}`
      );
      setRows(res.data);
    } catch {
      console.log("error");
    }
  };

  return (
    <Container>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
        <DemoContainer
          components={["DateTimePicker", "DateTimePicker", "Button"]}
        >
          <DateTimePicker
            label="Start"
            value={startDateTime}
            onChange={(newValue) => setStartDateTime(newValue)}
          />
          <DateTimePicker
            label="End"
            value={endDateTime}
            onChange={(newValue) => setEndDateTime(newValue)}
          />
          <Button variant="contained" onClick={getMessages}>
            Get messages
          </Button>
        </DemoContainer>
      </LocalizationProvider>
      {rows ? <DataTable rows={rows} /> : <></>}
    </Container>
  );
}

export default App;
