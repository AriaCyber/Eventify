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

  async function load() {
    const res = await api.get("/admin/events");
    setEvents(res.data);
  }

  useEffect(() => {
    load();
  }, []);

  async function addEvent() {
    try {
      await api.post("/admin/events", {
        title: title,
        description: "",
        startDateTime: "2025-01-01T18:00:00",
        endDateTime: "2025-01-01T21:00:00",
        capacity: capacity,
        remainingCapacity: capacity,
        pricePerTicket: price,
        isPublic: true
      });

      setTitle("");
      load();
    } catch (err: any) {
      alert(err?.response?.data || err.message);
    }
  }

  async function remove(id: number) {
    await api.delete(`/admin/events/${id}`);
    load();
  }

  return (
    <div>
      <h2>Manage Events</h2>

      <input
        placeholder="Event title"
        value={title}
        onChange={e => setTitle(e.target.value)}
      />

      <input
        type="number"
        placeholder="Price"
        value={price}
        onChange={e => setPrice(Number(e.target.value))}
      />

      <input
        type="number"
        placeholder="Capacity"
        value={capacity}
        onChange={e => setCapacity(Number(e.target.value))}
      />

      <button onClick={addEvent}>Add Event</button>

      <ul>
        {events.map(ev => (
          <li key={ev.id}>
            {ev.title} — ${ev.pricePerTicket}
            <button onClick={() => remove(ev.id)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}
