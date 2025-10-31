import { Box, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import type { TeamDto, TeamDtoPagedResult } from '../../../api/generated/client';
import { formatDDMMYYYY } from '../../../Helpers/CommonFuncitons';

interface Props {
    result: TeamDtoPagedResult | null;
}

export default function TeamSearchList({ result }: Props) {
    const rows = (result?.items ?? []) as TeamDto[];

    return (
        <Box sx={{ maxWidth: 1100, mx: "auto", mt: 3 }}>
            {!result ? null : rows.length === 0 ? (
                <Typography variant="body1" textAlign="center" sx={{ mt: 2 }}>
                    No results.
                </Typography>
            ) : (
                <TableContainer
                    component={Paper}
                    sx={{
                        maxHeight: "65vh",
                        borderRadius: 2,
                    }}
                >
                    <Table stickyHeader size="medium" aria-label="teams table">
                        <TableHead>
                            <TableRow>
                                <TableCell sx={{ fontWeight: 600 }}>Rank</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Name</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Mascot</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Wins %</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Wins</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Losses</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Ties</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Games</TableCell>
                                <TableCell sx={{ fontWeight: 600 }}>Last Win</TableCell>
                            </TableRow>
                        </TableHead>

                        <TableBody>
                            {rows.map((t) => (
                                <TableRow key={t.id} hover>
                                    <TableCell>{t.rank}</TableCell>
                                    <TableCell>{t.name}</TableCell>
                                    <TableCell>{t.mascot}</TableCell>
                                    <TableCell>
                                        {t.winsPercentage != null ? `${(t.winsPercentage * 100).toFixed(2)}%` : "—"}
                                    </TableCell>
                                    <TableCell>{t.wins}</TableCell>
                                    <TableCell>{t.losses}</TableCell>
                                    <TableCell>{t.ties}</TableCell>
                                    <TableCell>{t.games}</TableCell>
                                    <TableCell>
                                        {t.dateOfLastWin
                                            ? formatDDMMYYYY(new Date(t.dateOfLastWin))
                                            : "—"}
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Box>
    );
}