-- =========================================================================
-- SCRIPT 08: Bá»” SUNG 30 Máº¶T HĂ€NG Táº P HĂ“A BĂN Láºº & THIáº¾T Yáº¾U
-- Äá»“ng bá»™ hoĂ n toĂ n dá»¯ liá»‡u vá»›i ProductRepository.cs (71 máº·t hĂ ng)
-- HĂ¬nh áº£nh Packshot sáº¯c nĂ©t, chuáº©n font chá»¯, giĂ¡ cáº£ sĂ¡t quáº§y táº¡p hĂ³a thá»±c táº¿
-- =========================================================================

USE [KhoBanLe];
GO

SET NOCOUNT ON;

-- 42. AQ-500: NÆ°á»›c tinh khiáº¿t Aquafina chai 500ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'AQ-500')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'AQ-500', N'NÆ°á»›c tinh khiáº¿t Aquafina chai 500ml', N'Chai', N'NÆ°á»›c giáº£i khĂ¡t & Bia', 6000, 180, 24,
            N'NÆ°á»›c uá»‘ng tinh khiáº¿t Aquafina cĂ´ng nghá»‡ lá»c Hydro-7 thanh khiáº¿t sáº£ng khoĂ¡i.', N'images/products/aquafina-500ml.jpg');
END

-- 43. NB1-330: NÆ°á»›c tÄƒng lá»±c Number 1 chai thá»§y tinh 330ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'NB1-330')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'NB1-330', N'NÆ°á»›c tÄƒng lá»±c Number 1 chai thá»§y tinh 330ml', N'Chai', N'NÆ°á»›c giáº£i khĂ¡t & Bia', 10000, 120, 24,
            N'NÆ°á»›c tÄƒng lá»±c Number 1 chai thá»§y tinh náº¯p khoĂ©n báº­t vá»‹ ngon truyá»n thá»‘ng bá»«ng tá»‰nh nÄƒng lÆ°á»£ng.', N'images/products/number1-330ml.jpg');
END

-- 44. KD-455: TrĂ  xanh KhĂ´ng Äá»™ vá»‹ chanh chai 455ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'KD-455')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'KD-455', N'TrĂ  xanh KhĂ´ng Äá»™ vá»‹ chanh chai 455ml', N'Chai', N'NÆ°á»›c giáº£i khĂ¡t & Bia', 10000, 95, 24,
            N'TrĂ  xanh KhĂ´ng Äá»™ chiáº¿t xuáº¥t lĂ¡ trĂ  ThĂ¡i NguyĂªn dá»“i dĂ o EGCG giáº£i nhiá»‡t cuá»™c sá»‘ng.', N'images/products/khongdo-455ml.jpg');
END

-- 45. PEP-320: NÆ°á»›c ngá»t Pepsi vá»‹ nguyĂªn báº£n lon 320ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'PEP-320')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'PEP-320', N'NÆ°á»›c ngá»t Pepsi vá»‹ nguyĂªn báº£n lon 320ml', N'Lon', N'NÆ°á»›c giáº£i khĂ¡t & Bia', 11000, 140, 24,
            N'NÆ°á»›c giáº£i khĂ¡t cĂ³ ga Pepsi sáº£ng khoĂ¡i bĂ¹ng ná»• vá»‹ cola mĂ¡t láº¡nh cá»±c Ä‘Ă£.', N'images/products/pepsi-320ml.jpg');
END

-- 46. 7UP-320: NÆ°á»›c ngá»t 7Up vá»‹ chanh lon 320ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'7UP-320')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'7UP-320', N'NÆ°á»›c ngá»t 7Up vá»‹ chanh lon 320ml', N'Lon', N'NÆ°á»›c giáº£i khĂ¡t & Bia', 11000, 110, 24,
            N'NÆ°á»›c giáº£i khĂ¡t cĂ³ ga 7Up hÆ°Æ¡ng chanh tÆ°Æ¡i thanh mĂ¡t tiĂªu hĂ³a tá»‘t sáº£ng khoĂ¡i ngĂ y hĂ¨.', N'images/products/7up-320ml.jpg');
END

-- 47. SG-330: Bia SĂ i GĂ²n Special rá»“ng vĂ ng lon 330ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SG-330')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'SG-330', N'Bia SĂ i GĂ²n Special rá»“ng vĂ ng lon 330ml', N'Lon', N'NÆ°á»›c giáº£i khĂ¡t & Bia', 16000, 90, 24,
            N'Bia SĂ i GĂ²n Special 100% lĂºa máº¡ch háº£o háº¡ng hÆ°Æ¡ng vá»‹ Ä‘áº­m Ä‘Ă  khĂ³ quĂªn.', N'images/products/saigon-special.jpg');
END

-- 48. TP-327: NÆ°á»›c cam Ă©p Minute Maid Teppy tĂ©p cam 327ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TP-327')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'TP-327', N'NÆ°á»›c cam Ă©p Minute Maid Teppy tĂ©p cam 327ml', N'Chai', N'NÆ°á»›c giáº£i khĂ¡t & Bia', 12000, 80, 20,
            N'NÆ°á»›c cam Ă©p Teppy dá»“i dĂ o tĂ©p cam má»ng nÆ°á»›c thÆ¡m ngon giĂ u Vitamin C.', N'images/products/teppy-orange.jpg');
END

-- 49. MUOI-1KG: Muá»‘i tinh sáº¥y iot Báº¡c LiĂªu cao cáº¥p gĂ³i 1kg
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'MUOI-1KG')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'MUOI-1KG', N'Muá»‘i tinh sáº¥y iot Báº¡c LiĂªu cao cáº¥p gĂ³i 1kg', N'GĂ³i', N'Thá»±c pháº©m & Gia vá»‹', 6000, 150, 30,
            N'Muá»‘i Äƒn tinh sáº¥y háº¡t má»‹n bá»• sung vi cháº¥t I-á»‘t phĂ²ng ngá»«a bÆ°á»›u cá»• gĂ³i 1kg tiá»‡n lá»£i.', N'images/products/muoi-iot-1kg.jpg');
END

-- 50. DG-1KG: ÄÆ°á»ng cĂ¡t tráº¯ng BiĂªn HĂ²a Pure tinh luyá»‡n 1kg
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'DG-1KG')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'DG-1KG', N'ÄÆ°á»ng cĂ¡t tráº¯ng BiĂªn HĂ²a Pure tinh luyá»‡n 1kg', N'GĂ³i', N'Thá»±c pháº©m & Gia vá»‹', 28000, 90, 20,
            N'ÄÆ°á»ng tinh luyá»‡n BiĂªn HĂ²a Pure tá»« 100% mĂ­a Ä‘Æ°á»ng tá»± nhiĂªn ngá»t thanh tinh khiáº¿t.', N'images/products/duong-bienhoa-1kg.jpg');
END

-- 51. AJI-454: Bá»™t ngá»t Ajinomoto háº¡t lá»›n gĂ³i 454g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'AJI-454')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'AJI-454', N'Bá»™t ngá»t Ajinomoto háº¡t lá»›n gĂ³i 454g', N'GĂ³i', N'Thá»±c pháº©m & Gia vá»‹', 34000, 75, 15,
            N'MĂ¬ chĂ­nh Ajinomoto tinh khiáº¿t lĂªn men tá»± nhiĂªn tá»« máº­t mĂ­a vĂ  tinh bá»™t sáº¯n thÆ¡m ngon mĂ³n Äƒn.', N'images/products/ajinomoto-454g.jpg');
END

-- 52. TTT-500: NÆ°á»›c tÆ°Æ¡ng Tam ThĂ¡i Tá»­ Nháº¥t Ca sĂ¡nh Ä‘áº­m 500ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TTT-500')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'TTT-500', N'NÆ°á»›c tÆ°Æ¡ng Tam ThĂ¡i Tá»­ Nháº¥t Ca sĂ¡nh Ä‘áº­m 500ml', N'Chai', N'Thá»±c pháº©m & Gia vá»‹', 16000, 85, 20,
            N'NÆ°á»›c tÆ°Æ¡ng Tam ThĂ¡i Tá»­ Nháº¥t Ca Ä‘áº­u nĂ nh tá»± nhiĂªn khĂ´ng 3-MCPD cháº¥m hay xĂ o Ä‘á»u ngon.', N'images/products/tamthaitu-500ml.jpg');
END

-- 53. TC-155: CĂ¡ ná»¥c sá»‘t cĂ  Three Lady Cooks Ba CĂ´ GĂ¡i 155g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TC-155')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'TC-155', N'CĂ¡ ná»¥c sá»‘t cĂ  Three Lady Cooks Ba CĂ´ GĂ¡i 155g', N'Há»™p', N'Thá»±c pháº©m & Gia vá»‹', 17000, 95, 20,
            N'CĂ¡ ná»¥c sá»‘t cĂ  chua Ba CĂ´ GĂ¡i thá»‹t cĂ¡ cháº¯c ngá»t sá»‘t Ä‘áº­m Ä‘Ă  Äƒn liá»n tiá»‡n lá»£i.', N'images/products/bacogai-155g.jpg');
END

-- 54. XX-VS: XĂºc xĂ­ch tiá»‡t trĂ¹ng Vissan heo gĂ³i 5 cĂ¢y 175g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'XX-VS')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'XX-VS', N'XĂºc xĂ­ch tiá»‡t trĂ¹ng Vissan heo gĂ³i 5 cĂ¢y 175g', N'GĂ³i', N'Thá»±c pháº©m & Gia vá»‹', 22000, 80, 20,
            N'XĂºc xĂ­ch dinh dÆ°á»¡ng heo Vissan thÆ¡m bĂ¹i giĂ u Ä‘áº¡m Äƒn liá»n hoáº·c Äƒn kĂ¨m mĂ¬ tĂ´m.', N'images/products/xucxich-vissan.jpg');
END

-- 55. KM-90: MĂ¬ tĂ´m Kokomi Äáº¡i 90 tĂ´m chua cay gĂ³i 90g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'KM-90')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'KM-90', N'MĂ¬ tĂ´m Kokomi Äáº¡i 90 tĂ´m chua cay gĂ³i 90g', N'GĂ³i', N'Thá»±c pháº©m & Gia vá»‹', 4000, 250, 50,
            N'MĂ¬ Kokomi Äáº¡i 90 sá»£i mĂ¬ dai giĂ²n thÆ¡m bĂ¹i xá»‘t tĂ´m chua cay Ä‘áº­m Ä‘Ă  Äƒn no Ä‘Ă£ miá»‡ng.', N'images/products/kokomi-90g.jpg');
END

-- 56. DN-82: MĂ¬ Äá»‡ Nháº¥t thá»‹t báº±m rau cá»§ gĂ³i 82g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'DN-82')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'DN-82', N'MĂ¬ Äá»‡ Nháº¥t thá»‹t báº±m rau cá»§ gĂ³i 82g', N'GĂ³i', N'Thá»±c pháº©m & Gia vá»‹', 7500, 130, 30,
            N'MĂ¬ Äƒn liá»n Äá»‡ Nháº¥t Acecook nÆ°á»›c sĂºp ngá»t thanh thá»‹t báº±m bá»• sung tinh nghá»‡ thÆ¡m ngon.', N'images/products/denhat-82g.jpg');
END

-- 57. BM-KD: BĂ¡nh mĂ¬ tÆ°Æ¡i Kinh ÄĂ´ bÆ¡ sá»¯a thÆ¡m má»m 50g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'BM-KD')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'BM-KD', N'BĂ¡nh mĂ¬ tÆ°Æ¡i Kinh ÄĂ´ bÆ¡ sá»¯a thÆ¡m má»m 50g', N'GĂ³i', N'BĂ¡nh káº¹o & Äá»“ Äƒn váº·t', 8000, 60, 15,
            N'BĂ¡nh mĂ¬ hoa cĂºc bÆ¡ sá»¯a Kinh ÄĂ´ ruá»™t má»m thÆ¡m ngá»t bĂ©o bá»¯a sĂ¡ng tiá»‡n lá»£i.', N'images/products/banhmi-kinhdo.jpg');
END

-- 58. OO-150: BĂ¡nh gáº¡o One One vá»‹ ngá»t dá»‹u truyá»n thá»‘ng 150g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OO-150')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'OO-150', N'BĂ¡nh gáº¡o One One vá»‹ ngá»t dá»‹u truyá»n thá»‘ng 150g', N'GĂ³i', N'BĂ¡nh káº¹o & Äá»“ Äƒn váº·t', 26000, 70, 15,
            N'BĂ¡nh gáº¡o giĂ²n tan One One lĂ m tá»« 100% háº¡t gáº¡o thÆ¡m Viá»‡t Nam vá»‹ ngá»t dá»‹u thanh tao.', N'images/products/oneone-150g.jpg');
END

-- 59. NB-168: BĂ¡nh xá»‘p Nabati kem phĂ´ mai bĂ©o ngáº­y há»™p 168g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'NB-168')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'NB-168', N'BĂ¡nh xá»‘p Nabati kem phĂ´ mai bĂ©o ngáº­y há»™p 168g', N'Há»™p', N'BĂ¡nh káº¹o & Äá»“ Äƒn váº·t', 22000, 85, 20,
            N'BĂ¡nh quáº¿ xá»‘p Richeese Nabati ngáº­p trĂ n lá»›p kem phĂ´ mai thÆ¡m bĂ©o Ä‘áº­m Ä‘Ă  háº¥p dáº«n.', N'images/products/nabati-phomai.jpg');
END

-- 60. OR-133: BĂ¡nh quy káº¹p kem vani Oreo sĂ´cĂ´la gĂ³i 133g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'OR-133')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'OR-133', N'BĂ¡nh quy káº¹p kem vani Oreo sĂ´cĂ´la gĂ³i 133g', N'GĂ³i', N'BĂ¡nh káº¹o & Äá»“ Äƒn váº·t', 18000, 90, 20,
            N'BĂ¡nh quy sĂ´cĂ´la káº¹p kem vani Oreo thÆ¡m lá»«ng cháº¥m sá»¯a Äƒn cá»±c ngon miá»‡ng.', N'images/products/oreo-133g.jpg');
END

-- 61. CA-58: Káº¹o cao su Cool Air hÆ°Æ¡ng báº¡c hĂ  khuynh diá»‡p hÅ© 58.4g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CA-58')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CA-58', N'Káº¹o cao su Cool Air hÆ°Æ¡ng báº¡c hĂ  khuynh diá»‡p hÅ© 58.4g', N'HÅ©', N'BĂ¡nh káº¹o & Äá»“ Äƒn váº·t', 28000, 65, 15,
            N'Káº¹o cao su khĂ´ng Ä‘Æ°á»ng Cool Air sáº£ng khoĂ¡i the mĂ¡t tá»©c thĂ¬ cho hÆ¡i thá»Ÿ thÆ¡m tho.', N'images/products/coolair-58g.jpg');
END

-- 62. AL-120: Káº¹o viĂªn Alpenliebe hÆ°Æ¡ng dĂ¢u kem ngá»t ngĂ o gĂ³i 120g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'AL-120')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'AL-120', N'Káº¹o viĂªn Alpenliebe hÆ°Æ¡ng dĂ¢u kem ngá»t ngĂ o gĂ³i 120g', N'GĂ³i', N'BĂ¡nh káº¹o & Äá»“ Äƒn váº·t', 14000, 95, 20,
            N'Káº¹o ngáº­m Alpenliebe hÆ°Æ¡ng dĂ¢u sá»¯a ngá»t ngĂ o bĂ©o ngáº­y mĂ³n Äƒn váº·t Ä‘Æ°á»£c má»i lá»©a tuá»•i yĂªu thĂ­ch.', N'images/products/alpenliebe-dau.jpg');
END

-- 63. ML-180: Sá»¯a lĂºa máº¡ch NestlĂ© Milo Activ-Go 180ml (lá»‘c 4 há»™p)
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'ML-180')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'ML-180', N'Sá»¯a lĂºa máº¡ch NestlĂ© Milo Activ-Go 180ml (lá»‘c 4 há»™p)', N'Lá»‘c', N'Sá»¯a & Cháº¿ pháº©m sá»¯a', 32000, 85, 20,
            N'Sá»¯a lĂºa máº¡ch Milo giĂ u dinh dÆ°á»¡ng vitamin khoĂ¡ng cháº¥t tiáº¿p thĂªm nÄƒng lÆ°á»£ng bá»n bá»‰.', N'images/products/milo-180ml.jpg');
END

-- 64. TH-180: Sá»¯a tÆ°Æ¡i TH true MILK Ă­t Ä‘Æ°á»ng 180ml (lá»‘c 4 há»™p)
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'TH-180')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'TH-180', N'Sá»¯a tÆ°Æ¡i TH true MILK Ă­t Ä‘Æ°á»ng 180ml (lá»‘c 4 há»™p)', N'Lá»‘c', N'Sá»¯a & Cháº¿ pháº©m sá»¯a', 36000, 75, 20,
            N'Sá»¯a tÆ°Æ¡i tiá»‡t trĂ¹ng TH true MILK nguyĂªn cháº¥t tá»« trang tráº¡i sáº¡ch chuáº©n quá»‘c táº¿.', N'images/products/th-truemilk-180ml.jpg');
END

-- 65. PB-65: Sá»¯a chua uá»‘ng men sá»‘ng Probi Vinamilk (lá»‘c 5 chai x 65ml)
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'PB-65')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'PB-65', N'Sá»¯a chua uá»‘ng men sá»‘ng Probi Vinamilk (lá»‘c 5 chai x 65ml)', N'Lá»‘c', N'Sá»¯a & Cháº¿ pháº©m sá»¯a', 26000, 70, 15,
            N'Sá»¯a chua uá»‘ng Probi chá»©a 65 tá»· lá»£i khuáº©n tÄƒng cÆ°á»ng Ä‘á» khĂ¡ng há»— trá»£ tiĂªu hĂ³a khá»e máº¡nh.', N'images/products/probi-65ml.jpg');
END

-- 66. LB-90: XĂ  bĂ´ng cá»¥c Lifebuoy báº£o vá»‡ vÆ°á»£t trá»™i 90g
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'LB-90')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'LB-90', N'XĂ  bĂ´ng cá»¥c Lifebuoy báº£o vá»‡ vÆ°á»£t trá»™i 90g', N'Cá»¥c', N'HĂ³a má»¹ pháº©m & Äá»“ gia dá»¥ng', 16000, 100, 25,
            N'XĂ  phĂ²ng bĂ¡nh Lifebuoy ion báº¡c báº£o vá»‡ khá»i 99.9% vi khuáº©n gĂ¢y bá»‡nh an toĂ n cáº£ nhĂ .', N'images/products/lifebuoy-soap.jpg');
END

-- 67. VIM-880: NÆ°á»›c táº©y bá»“n cáº§u Vim diá»‡t khuáº©n Ä‘áº­m Ä‘áº·c xanh 880ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'VIM-880')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'VIM-880', N'NÆ°á»›c táº©y bá»“n cáº§u Vim diá»‡t khuáº©n Ä‘áº­m Ä‘áº·c xanh 880ml', N'Chai', N'HĂ³a má»¹ pháº©m & Äá»“ gia dá»¥ng', 38000, 50, 12,
            N'NÆ°á»›c táº©y bá»“n cáº§u Vim cĂ´ng thá»©c diá»‡t sáº¡ch máº§m bá»‡nh táº©y sáº¡ch máº£ng bĂ¡m á»‘ vĂ ng nhanh chĂ³ng.', N'images/products/vim-880ml.jpg');
END

-- 68. SL-LS: NÆ°á»›c lau sĂ n Sunlight tinh dáº§u hoa quáº¿ chai 1kg
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'SL-LS')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'SL-LS', N'NÆ°á»›c lau sĂ n Sunlight tinh dáº§u hoa quáº¿ chai 1kg', N'Chai', N'HĂ³a má»¹ pháº©m & Äá»“ gia dá»¥ng', 34000, 45, 12,
            N'NÆ°á»›c lau sĂ n Sunlight hoáº¡t cháº¥t táº©y sáº¡ch sĂ¡ng bĂ³ng lÆ°u hÆ°Æ¡ng quáº¿ tá»± nhiĂªn xua Ä‘uá»•i cĂ´n trĂ¹ng.', N'images/products/sunlight-lausan.jpg');
END

-- 69. LB-450: NÆ°á»›c rá»­a tay Lifebuoy báº£o vá»‡ vÆ°á»£t trá»™i tĂºi 450ml
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'LB-450')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'LB-450', N'NÆ°á»›c rá»­a tay Lifebuoy báº£o vá»‡ vÆ°á»£t trá»™i tĂºi 450ml', N'TĂºi', N'HĂ³a má»¹ pháº©m & Äá»“ gia dá»¥ng', 65000, 40, 10,
            N'NÆ°á»›c rá»­a tay diá»‡t khuáº©n Lifebuoy tĂºi tiáº¿t kiá»‡m báº£o vá»‡ bĂ n tay má»m máº¡i sáº¡ch khuáº©n trong 10 giĂ¢y.', N'images/products/lifebuoy-handwash.jpg');
END

-- 70. EM-10: KhÄƒn giáº¥y vá»‡ sinh E'mos Classic 2 lá»›p lá»‘c 10 cuá»™n
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'EM-10')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'EM-10', N'KhÄƒn giáº¥y vá»‡ sinh E''mos Classic 2 lá»›p lá»‘c 10 cuá»™n', N'Lá»‘c', N'HĂ³a má»¹ pháº©m & Äá»“ gia dá»¥ng', 72000, 35, 10,
            N'Giáº¥y vá»‡ sinh cuá»™n E''mos Classic 100% bá»™t giáº¥y tá»± nhiĂªn má»m má»‹n dai vĂ  tháº¥m hĂºt tá»‘t.', N'images/products/emos-10cuon.jpg');
END

-- 71. CG-BC: BĂ n cháº£i Ä‘Ă¡nh rÄƒng Colgate Slim Soft than hoáº¡t tĂ­nh
IF NOT EXISTS (SELECT 1 FROM dbo.Product WHERE Sku = N'CG-BC')
BEGIN
    INSERT INTO dbo.Product (Sku, Name, Unit, Category, Price, CurrentStock, MinStockLevel, Description, ImageUrl)
    VALUES (N'CG-BC', N'BĂ n cháº£i Ä‘Ă¡nh rÄƒng Colgate Slim Soft than hoáº¡t tĂ­nh', N'CĂ¡i', N'HĂ³a má»¹ pháº©m & Äá»“ gia dá»¥ng', 22000, 80, 20,
            N'BĂ n cháº£i Colgate lĂ´ng cháº£i siĂªu má»m máº£nh phá»§ than hoáº¡t tĂ­nh len sĂ¢u káº½ rÄƒng lĂ m sáº¡ch hiá»‡u quáº£.', N'images/products/colgate-banchai.jpg');
END

GO

SELECT COUNT(*) AS TotalProducts FROM dbo.Product;
GO
