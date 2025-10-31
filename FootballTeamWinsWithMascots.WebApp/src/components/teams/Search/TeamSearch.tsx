import { useState } from "react";
import { type TeamDtoPagedResult } from "../../../api/generated/client";
import TeamSearhList from "./TeamSearhList";
import TeamSearchForm from "./TeamSearchForm";

export default function TeamsSearchPage() {
    const [loading, setLoading] = useState(false);
    const [result, setResult] = useState<TeamDtoPagedResult | null>(null);

    return (
        <>
            <TeamSearchForm loading={loading} setLoading={setLoading} setResult={setResult} />
            <TeamSearhList result={result} />
        </>
    );
}