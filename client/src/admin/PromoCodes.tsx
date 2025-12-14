import { useEffect, useState } from "react";
import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5110/api",
});

export default function PromoCodes() {
  const [codes, setCodes] = useState<any[]>([]);
  const [code, setCode] = useState("");
  const [discountValue, setDiscountValue] = useState(10);
  const [isPercentage, setIsPercentage] = useState(true);

  async function load() {
    const res = await api.get("/admin/promocodes");
    setCodes(res.data);
  }

  useEffect(() => {
    load();
  }, []);

  async function addPromo() {
    try {
      await api.post("/admin/promocodes", {
        code: code,
        discountValue: discountValue,
        isPercentage: isPercentage,
        usageLimit: 100,
        expiryDate: "2025-12-31T00:00:00"
      });

      setCode("");
      load();
    } catch (err: any) {
      alert(err?.response?.data || err.message);
    }
  }

  return (
    <div>
      <h2>Promo Codes</h2>

      <input
        placeholder="Code"
        value={code}
        onChange={e => setCode(e.target.value)}
      />

      <input
        type="number"
        placeholder="Discount"
        value={discountValue}
        onChange={e => setDiscountValue(Number(e.target.value))}
      />

      <label>
        <input
          type="checkbox"
          checked={isPercentage}
          onChange={e => setIsPercentage(e.target.checked)}
        />
        Percentage
      </label>

      <button onClick={addPromo}>Add Promo Code</button>

      <ul>
        {codes.map(p => (
          <li key={p.id}>
            {p.code} — {p.discountValue}{p.isPercentage ? "%" : "$"}
          </li>
        ))}
      </ul>
    </div>
  );
}
