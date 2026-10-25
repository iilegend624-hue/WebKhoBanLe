$scriptPath = Join-Path $PSScriptRoot "09_expand_to_over_100_retail_products.sql"
$sql = [System.IO.File]::ReadAllText($scriptPath, [System.Text.Encoding]::UTF8)

# Delete existing rows with these SKUs first so we can re-insert with crystal clean Vietnamese Unicode
$skusToDelete = @(
    'STR-330', 'RB-250', 'C2-360', 'LV-500', 'HN-330',
    'VM-SC100', 'OT-380', 'TH-1L', 'ML-BOX',
    'OMA-SUON', 'OMA-BO', 'HH-STH', 'PHO-CD', 'MI-CUP-HH',
    'SIM-1L', 'NEP-1L', 'CS-OT', 'NN-750', 'KNR-400', 'CS-TUONG',
    'CP-12', 'CS-MARIE', 'LAY-NAT', 'OST-KC', 'SW-STEAK', 'CP-PANDA',
    'G7-3IN1', 'NES-3IN1', 'TN-CHUS', 'LIP-YEL',
    'CLR-630', 'SUN-650', 'DOVE-640', 'PS-200', 'CG-TOT', 'ROM-650',
    'OMO-36', 'ARI-32', 'COMF-18', 'SL-750',
    'PULP-10', 'PULP-BOX', 'HEO-VS', 'PATE-HL'
)

$conn = New-Object System.Data.SqlClient.SqlConnection("Server=localhost;Database=KhoBanLe;Trusted_Connection=True;")
$conn.Open()

$inClause = ($skusToDelete | ForEach-Object { "'$_'" }) -join ","
$delCmd = $conn.CreateCommand()
$delCmd.CommandText = "DELETE FROM dbo.Product WHERE Sku IN ($inClause);"
$delRows = $delCmd.ExecuteNonQuery()
Write-Host "Cleared $delRows temporary rows."

$batches = $sql -split "(?m)^\s*GO\s*$"
foreach ($b in $batches) {
    $trimmed = $b.Trim()
    if ($trimmed.Length -gt 0) {
        $cmd = $conn.CreateCommand()
        $cmd.CommandText = $trimmed
        [void]$cmd.ExecuteNonQuery()
    }
}

$countCmd = $conn.CreateCommand()
$countCmd.CommandText = "SELECT COUNT(*) FROM dbo.Product;"
$total = $countCmd.ExecuteScalar()
Write-Host "Total products in SQL Server after clean Unicode insert: $total"

$conn.Close()
