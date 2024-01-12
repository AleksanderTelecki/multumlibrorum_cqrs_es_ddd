@echo off
cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Client.Endpoint\bin\Debug\net6.0
start Client.Endpoint.exe --urls "https://localhost:7007;http://localhost:5175"

cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Sales.Endpoint\bin\Debug\net6.0
start Sales.Endpoint.exe --urls "https://localhost:7039;http://localhost:5107"

cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Product.Endpoint\bin\Debug\net6.0
start Product.Endpoint.exe --urls "https://localhost:7125;http://localhost:5096"

cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Promotion.Endpoint\bin\Debug\net6.0
start Promotion.Endpoint.exe --urls "https://localhost:7135;http://localhost:5267"

cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Payment.Endpoint\bin\Debug\net6.0
start Payment.Endpoint.exe --urls "https://localhost:7268;http://localhost:5038"

cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Employee.Endpoint\bin\Debug\net6.0
start Employee.Endpoint.exe --urls "https://localhost:7062;http://localhost:5028"

cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Document.Endpoint\bin\Debug\net6.0
start Document.Endpoint.exe --urls "https://localhost:7005;http://localhost:5048"

cd /d E:\Uni\Multum Librorum\Multum Librorum\src\Communication.Endpoint\bin\Debug\net6.0
start Communication.Endpoint.exe --urls "https://localhost:7030;http://localhost:5166"