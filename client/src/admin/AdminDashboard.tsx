import { useEffect, useState } from "react";
import { getSalesReport, getOccupancyReport } from "./adminApi";

export default function AdminDashboard(){
  const [sales, setSales] = useState<any[]>([]);
  const [occupancy, setOccupancy] = useState<any[]>([]);

  useEffect(() => {
    getSalesReport().then(setSales);
    getOccupancyReport().then(setOccupancy);
  }, []);

  return (
    <div>
      <h2>Admin Dashboard</h2>

      <h3>Sales Report</h3>
      <ul>
        {sales.map(s => (
          <li key={s.date}>
            {s.date} — {s.ticketsSold} tickets — ${s.totalRevenue}
          </li>
        ))}
      </ul>

      <h3>Event Occupancy</h3>
      <ul>
        {occupancy.map(o => (
          <li key={o.eventId}>
            {o.eventTitle}: {o.occupancyRate.toFixed(1)}%
          </li>
        ))}
      </ul>
    </div>
  );
}
