import { useEffect, useState } from "react";
import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5110/api",
});

export default function ManageEvents() {
  const [events, setEvents] = useState<any[]>([]);

  const [title, setTitle] = useState("");
  const [price, setPrice] = useState(20);
  const [capacity, setCapacity] = useState(100);
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");
  const [isPublic, setIsPublic] = useState(true);

  async function load() {
    const res = await api.get("/admin/events");
    setEvents(res.data);
  }

  useEffect(() => {
    load();
  }, []);

  async function addEvent() {
    if (!title || !startDate || !endDate) {
      alert("Please fill all required fields");
      return;
    }

    try {
      await api.post("/admin/events", {
        title,
        description: "",
        startDateTime: startDate,
        endDateTime: endDate,
        capacity,
        remainingCapacity: capacity,
        pricePerTicket: price,
        isPublic,
      });

      // reset form
      setTitle("");
      setStartDate("");
      setEndDate("");

      load();
    } catch (e: any) {
      alert(e?.response?.data || e.message);
    }
  }

  async function remove(id: number) {
    await api.delete(`/admin/events/${id}`);
    load();
  }

  return (
    <div>
      <h2>Manage Events</h2>

      <label>
        Event Title
        <input
          placeholder="Concert, Conference, Meetup..."
          value={title}
          onChange={(e) => setTitle(e.target.value)}
        />
      </label>

      <label>
        Ticket Price ($)
        <input
          type="number"
          min={0}
          value={price}
          onChange={(e) => setPrice(Number(e.target.value))}
        />
      </label>

      <label>
        Capacity
        <input
          type="number"
          min={1}
          value={capacity}
          onChange={(e) => setCapacity(Number(e.target.value))}
        />
      </label>

      <label>
        Start Date & Time
        <input
          type="datetime-local"
          value={startDate}
          onChange={(e) => setStartDate(e.target.value)}
        />
      </label>

      <label>
        End Date & Time
        <input
          type="datetime-local"
          value={endDate}
          onChange={(e) => setEndDate(e.target.value)}
        />
      </label>

      <label style={{ display: "flex", gap: "8px", alignItems: "center" }}>
        <input
          type="checkbox"
          checked={isPublic}
          onChange={(e) => setIsPublic(e.target.checked)}
        />
        Public Event
      </label>

      <button onClick={addEvent}>Add Event</button>

      <hr />

      <ul>
        {events.map((ev) => (
          <li key={ev.id}>
            <strong>{ev.title}</strong> — ${ev.pricePerTicket} —{" "}
            {new Date(ev.startDateTime).toLocaleString()}
            <button onClick={() => remove(ev.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
