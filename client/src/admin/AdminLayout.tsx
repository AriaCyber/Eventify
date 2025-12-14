import { Link, Outlet } from "react-router-dom";

export default function AdminLayout() {
  return (
    <div style={{ display: "flex", gap: "24px" }}>
      <aside style={{ minWidth: 200 }}>
        <h3>Admin Panel</h3>
        <ul style={{ listStyle: "none", padding: 0 }}>
          <li><Link to="/admin">Dashboard</Link></li>
          <li><Link to="/admin/events">Events</Link></li>
          <li><Link to="/admin/promocodes">Promo Codes</Link></li>
          <li><Link to="/admin/refunds">Refunds</Link></li>
        </ul>
      </aside>

      <main style={{ flex: 1 }}>
        <Outlet />
      </main>
    </div>
  );
}
