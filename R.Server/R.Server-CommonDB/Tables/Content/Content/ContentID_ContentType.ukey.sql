﻿ALTER TABLE Content_Content
ADD CONSTRAINT UK_Content_Content_ContentID_ContentType
UNIQUE (ContentID, ContentType)