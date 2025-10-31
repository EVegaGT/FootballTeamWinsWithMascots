import { Box, Typography } from '@mui/material';
import type { TeamDtoPagedResult } from '../../../api/generated/client';
import { DataGrid, type GridColDef } from '@mui/x-data-grid';

interface Props {
    result: TeamDtoPagedResult | null;
}

export default function TeamSearchList({ result }: Props) {
    const columns: GridColDef[] = [
        { field: 'rank', headerName: 'Rank', width: 50 },
        { field: 'name', headerName: 'Name', width: 200, flex: 1 },
        { field: 'mascot', headerName: 'Mascot', width: 200, flex: 1 },
        {
            field: 'winsPercentage',
            headerName: 'Wins %',
            width: 110,
            valueFormatter: (p: number) =>
                p != null ? (p * 100).toFixed(2) + '%' : '-',
        },
        { field: 'wins', headerName: 'Wins', width: 70 },
        { field: 'losses', headerName: 'Losses', width: 70 },
        { field: 'ties', headerName: 'Ties', width: 70 },
        { field: 'games', headerName: 'Games', width: 70 },
        {
            field: 'dateOfLastWin',
            headerName: 'Last Win',
            width: 150,
            valueFormatter: (params) => {
                if (!params) return '-';
                const date = new Date(params);
                const day = String(date.getDate()).padStart(2, '0');
                const month = String(date.getMonth() + 1).padStart(2, '0');
                const year = date.getFullYear();
                return `${day}/${month}/${year}`;
            },
        },
    ];

    return (
        <Box sx={{ maxWidth: 1500, mx: 'auto', mt: 4 }}>
            {result?.items?.length ? (
                <DataGrid
                    rows={result.items.map((t) => ({
                        id: t.id,
                        ...t,
                    }))}
                    columns={columns}
                    pageSizeOptions={[5, 10, 20]}
                    initialState={{
                        pagination: {
                            paginationModel: {
                                pageSize: result.pageSize || 10,
                            },
                        },
                    }}
                    autoHeight
                    disableRowSelectionOnClick
                />
            ) : (
                result && (
                    <Typography variant="body1" textAlign="center" sx={{ mt: 2 }}>
                        No results found.
                    </Typography>
                )
            )}
        </Box>
    );
}