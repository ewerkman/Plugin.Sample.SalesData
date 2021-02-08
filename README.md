This plugin will export sales data to a table.

Before using please test thoroughly. 

> Note that this plugin does not check if the sales data has already been exported.

Create the table using the following sql:
```tsql
CREATE TABLE [dbo].[Sales](
	[ProductID] [nvarchar](50) NOT NULL,
	[VariantID] [nvarchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderId] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Sales_1] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC,
	[VariantID] ASC,
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```
