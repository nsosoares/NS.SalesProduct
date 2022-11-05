CREATE TABLE Customer (
   "Id" uuid PRIMARY KEY,
   "Name" VARCHAR(150) NOT NULL,
   "Cpf" VARCHAR(14) NOT NULL,
   "BirthDate" TIMESTAMP(6) NOT NULL
);

CREATE TABLE Product (
   "Id" uuid PRIMARY KEY,
   "Name" VARCHAR(150) NOT NULL,
   "Price" DECIMAL(18, 2) NOT NULL,
   "CostPrice" DECIMAL(18, 2) NOT NULL
);

CREATE TABLE SaleItem (
   "Id" uuid PRIMARY KEY,
   "ProductId" uuid NOT NULl,
   "Amount" int NOT NULL,
   "SaleId" uuid  NOT NULl,
	FOREIGN KEY ("ProductId") 
		REFERENCES Product ("Id"),
	FOREIGN KEY ("SaleId")
		REFERENCES Sale ("Id")
);

CREATE TABLE Sale (
   "Id" uuid PRIMARY KEY,
   "CustomerId" uuid NOT NULl,
   FOREIGN KEY ("CustomerId")
	 REFERENCES Customer ("Id"),
   "TotalPrice" DECIMAL(18, 2) NOT NULL,
   "Discount" int,
   "PaymentMethod" int NOT NULL,
   "PricePaid" DECIMAL(18, 2) NOT NULL
);