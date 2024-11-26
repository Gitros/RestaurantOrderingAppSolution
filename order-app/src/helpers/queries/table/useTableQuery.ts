import { useQuery } from "@tanstack/react-query";
import { Table } from "../../../models/table";
import apiClient from "../../../utils/apiClient";

const fetchTables = async (): Promise<Table[]> => {
    const { data } = await apiClient.get("/table");
    return data;
}

export const useTableQuery = () => {
    return useQuery({
        queryKey: ['getAllTables'],
        queryFn: fetchTables
    });
};