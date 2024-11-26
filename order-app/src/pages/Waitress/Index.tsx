import axios from "axios";
import { useEffect, useState } from "react";

export default function WaitressPage() {
    const [tables, setTables] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5000/api/table')
            .then(response => {
                setTables(response.data)
            })
    }, [])

    return (
        <div>
            <h1>Tables</h1>
            <ul>
                {tables.map((table: any) => (
                    <li key={table.id}>
                        {table.name}
                    </li>
                ))}
            </ul>
        </div>
    );
}