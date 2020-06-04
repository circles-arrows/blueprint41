using System;
using System.Collections.Generic;
using System.Linq;

using Blueprint41.Core;
using Blueprint41.TypeConversion;

#region Assembly Conversion Registration

[assembly: Conversion(typeof(ListOfObjectToListOfBool))]
[assembly: Conversion(typeof(ListOfBoolToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfBoolNullable))]
[assembly: Conversion(typeof(ListOfBoolNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfSbyte))]
[assembly: Conversion(typeof(ListOfSbyteToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfSbyteNullable))]
[assembly: Conversion(typeof(ListOfSbyteNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfShort))]
[assembly: Conversion(typeof(ListOfShortToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfShortNullable))]
[assembly: Conversion(typeof(ListOfShortNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfInt))]
[assembly: Conversion(typeof(ListOfIntToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfIntNullable))]
[assembly: Conversion(typeof(ListOfIntNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfLong))]
[assembly: Conversion(typeof(ListOfLongToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfLongNullable))]
[assembly: Conversion(typeof(ListOfLongNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfFloat))]
[assembly: Conversion(typeof(ListOfFloatToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfFloatNullable))]
[assembly: Conversion(typeof(ListOfFloatNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfDouble))]
[assembly: Conversion(typeof(ListOfDoubleToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfDoubleNullable))]
[assembly: Conversion(typeof(ListOfDoubleNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfChar))]
[assembly: Conversion(typeof(ListOfCharToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfCharNullable))]
[assembly: Conversion(typeof(ListOfCharNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfString))]
[assembly: Conversion(typeof(ListOfStringToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfDateTime))]
[assembly: Conversion(typeof(ListOfDateTimeToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfDateTimeNullable))]
[assembly: Conversion(typeof(ListOfDateTimeNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfGuid))]
[assembly: Conversion(typeof(ListOfGuidToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfGuidNullable))]
[assembly: Conversion(typeof(ListOfGuidNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfDecimal))]
[assembly: Conversion(typeof(ListOfDecimalToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfDecimalNullable))]
[assembly: Conversion(typeof(ListOfDecimalNullableToListOfObject))]
[assembly: Conversion(typeof(ListOfObjectToListOfCompressedString))]
[assembly: Conversion(typeof(ListOfCompressedStringToListOfObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolBool))]
[assembly: Conversion(typeof(DictOfBoolBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolBoolNullable))]
[assembly: Conversion(typeof(DictOfBoolBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolSbyte))]
[assembly: Conversion(typeof(DictOfBoolSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolSbyteNullable))]
[assembly: Conversion(typeof(DictOfBoolSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolShort))]
[assembly: Conversion(typeof(DictOfBoolShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolShortNullable))]
[assembly: Conversion(typeof(DictOfBoolShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolInt))]
[assembly: Conversion(typeof(DictOfBoolIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolIntNullable))]
[assembly: Conversion(typeof(DictOfBoolIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolLong))]
[assembly: Conversion(typeof(DictOfBoolLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolLongNullable))]
[assembly: Conversion(typeof(DictOfBoolLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolFloat))]
[assembly: Conversion(typeof(DictOfBoolFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolFloatNullable))]
[assembly: Conversion(typeof(DictOfBoolFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolDouble))]
[assembly: Conversion(typeof(DictOfBoolDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolDoubleNullable))]
[assembly: Conversion(typeof(DictOfBoolDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolChar))]
[assembly: Conversion(typeof(DictOfBoolCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolCharNullable))]
[assembly: Conversion(typeof(DictOfBoolCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolString))]
[assembly: Conversion(typeof(DictOfBoolStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolDateTime))]
[assembly: Conversion(typeof(DictOfBoolDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolDateTimeNullable))]
[assembly: Conversion(typeof(DictOfBoolDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolGuid))]
[assembly: Conversion(typeof(DictOfBoolGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolGuidNullable))]
[assembly: Conversion(typeof(DictOfBoolGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolDecimal))]
[assembly: Conversion(typeof(DictOfBoolDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolDecimalNullable))]
[assembly: Conversion(typeof(DictOfBoolDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfBoolCompressedString))]
[assembly: Conversion(typeof(DictOfBoolCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteBool))]
[assembly: Conversion(typeof(DictOfSbyteBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteBoolNullable))]
[assembly: Conversion(typeof(DictOfSbyteBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteSbyte))]
[assembly: Conversion(typeof(DictOfSbyteSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteSbyteNullable))]
[assembly: Conversion(typeof(DictOfSbyteSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteShort))]
[assembly: Conversion(typeof(DictOfSbyteShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteShortNullable))]
[assembly: Conversion(typeof(DictOfSbyteShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteInt))]
[assembly: Conversion(typeof(DictOfSbyteIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteIntNullable))]
[assembly: Conversion(typeof(DictOfSbyteIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteLong))]
[assembly: Conversion(typeof(DictOfSbyteLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteLongNullable))]
[assembly: Conversion(typeof(DictOfSbyteLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteFloat))]
[assembly: Conversion(typeof(DictOfSbyteFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteFloatNullable))]
[assembly: Conversion(typeof(DictOfSbyteFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteDouble))]
[assembly: Conversion(typeof(DictOfSbyteDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteDoubleNullable))]
[assembly: Conversion(typeof(DictOfSbyteDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteChar))]
[assembly: Conversion(typeof(DictOfSbyteCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteCharNullable))]
[assembly: Conversion(typeof(DictOfSbyteCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteString))]
[assembly: Conversion(typeof(DictOfSbyteStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteDateTime))]
[assembly: Conversion(typeof(DictOfSbyteDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteDateTimeNullable))]
[assembly: Conversion(typeof(DictOfSbyteDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteGuid))]
[assembly: Conversion(typeof(DictOfSbyteGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteGuidNullable))]
[assembly: Conversion(typeof(DictOfSbyteGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteDecimal))]
[assembly: Conversion(typeof(DictOfSbyteDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteDecimalNullable))]
[assembly: Conversion(typeof(DictOfSbyteDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfSbyteCompressedString))]
[assembly: Conversion(typeof(DictOfSbyteCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortBool))]
[assembly: Conversion(typeof(DictOfShortBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortBoolNullable))]
[assembly: Conversion(typeof(DictOfShortBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortSbyte))]
[assembly: Conversion(typeof(DictOfShortSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortSbyteNullable))]
[assembly: Conversion(typeof(DictOfShortSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortShort))]
[assembly: Conversion(typeof(DictOfShortShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortShortNullable))]
[assembly: Conversion(typeof(DictOfShortShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortInt))]
[assembly: Conversion(typeof(DictOfShortIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortIntNullable))]
[assembly: Conversion(typeof(DictOfShortIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortLong))]
[assembly: Conversion(typeof(DictOfShortLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortLongNullable))]
[assembly: Conversion(typeof(DictOfShortLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortFloat))]
[assembly: Conversion(typeof(DictOfShortFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortFloatNullable))]
[assembly: Conversion(typeof(DictOfShortFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortDouble))]
[assembly: Conversion(typeof(DictOfShortDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortDoubleNullable))]
[assembly: Conversion(typeof(DictOfShortDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortChar))]
[assembly: Conversion(typeof(DictOfShortCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortCharNullable))]
[assembly: Conversion(typeof(DictOfShortCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortString))]
[assembly: Conversion(typeof(DictOfShortStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortDateTime))]
[assembly: Conversion(typeof(DictOfShortDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortDateTimeNullable))]
[assembly: Conversion(typeof(DictOfShortDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortGuid))]
[assembly: Conversion(typeof(DictOfShortGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortGuidNullable))]
[assembly: Conversion(typeof(DictOfShortGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortDecimal))]
[assembly: Conversion(typeof(DictOfShortDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortDecimalNullable))]
[assembly: Conversion(typeof(DictOfShortDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfShortCompressedString))]
[assembly: Conversion(typeof(DictOfShortCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntBool))]
[assembly: Conversion(typeof(DictOfIntBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntBoolNullable))]
[assembly: Conversion(typeof(DictOfIntBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntSbyte))]
[assembly: Conversion(typeof(DictOfIntSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntSbyteNullable))]
[assembly: Conversion(typeof(DictOfIntSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntShort))]
[assembly: Conversion(typeof(DictOfIntShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntShortNullable))]
[assembly: Conversion(typeof(DictOfIntShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntInt))]
[assembly: Conversion(typeof(DictOfIntIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntIntNullable))]
[assembly: Conversion(typeof(DictOfIntIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntLong))]
[assembly: Conversion(typeof(DictOfIntLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntLongNullable))]
[assembly: Conversion(typeof(DictOfIntLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntFloat))]
[assembly: Conversion(typeof(DictOfIntFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntFloatNullable))]
[assembly: Conversion(typeof(DictOfIntFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntDouble))]
[assembly: Conversion(typeof(DictOfIntDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntDoubleNullable))]
[assembly: Conversion(typeof(DictOfIntDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntChar))]
[assembly: Conversion(typeof(DictOfIntCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntCharNullable))]
[assembly: Conversion(typeof(DictOfIntCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntString))]
[assembly: Conversion(typeof(DictOfIntStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntDateTime))]
[assembly: Conversion(typeof(DictOfIntDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntDateTimeNullable))]
[assembly: Conversion(typeof(DictOfIntDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntGuid))]
[assembly: Conversion(typeof(DictOfIntGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntGuidNullable))]
[assembly: Conversion(typeof(DictOfIntGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntDecimal))]
[assembly: Conversion(typeof(DictOfIntDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntDecimalNullable))]
[assembly: Conversion(typeof(DictOfIntDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfIntCompressedString))]
[assembly: Conversion(typeof(DictOfIntCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongBool))]
[assembly: Conversion(typeof(DictOfLongBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongBoolNullable))]
[assembly: Conversion(typeof(DictOfLongBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongSbyte))]
[assembly: Conversion(typeof(DictOfLongSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongSbyteNullable))]
[assembly: Conversion(typeof(DictOfLongSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongShort))]
[assembly: Conversion(typeof(DictOfLongShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongShortNullable))]
[assembly: Conversion(typeof(DictOfLongShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongInt))]
[assembly: Conversion(typeof(DictOfLongIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongIntNullable))]
[assembly: Conversion(typeof(DictOfLongIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongLong))]
[assembly: Conversion(typeof(DictOfLongLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongLongNullable))]
[assembly: Conversion(typeof(DictOfLongLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongFloat))]
[assembly: Conversion(typeof(DictOfLongFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongFloatNullable))]
[assembly: Conversion(typeof(DictOfLongFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongDouble))]
[assembly: Conversion(typeof(DictOfLongDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongDoubleNullable))]
[assembly: Conversion(typeof(DictOfLongDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongChar))]
[assembly: Conversion(typeof(DictOfLongCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongCharNullable))]
[assembly: Conversion(typeof(DictOfLongCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongString))]
[assembly: Conversion(typeof(DictOfLongStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongDateTime))]
[assembly: Conversion(typeof(DictOfLongDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongDateTimeNullable))]
[assembly: Conversion(typeof(DictOfLongDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongGuid))]
[assembly: Conversion(typeof(DictOfLongGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongGuidNullable))]
[assembly: Conversion(typeof(DictOfLongGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongDecimal))]
[assembly: Conversion(typeof(DictOfLongDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongDecimalNullable))]
[assembly: Conversion(typeof(DictOfLongDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfLongCompressedString))]
[assembly: Conversion(typeof(DictOfLongCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatBool))]
[assembly: Conversion(typeof(DictOfFloatBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatBoolNullable))]
[assembly: Conversion(typeof(DictOfFloatBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatSbyte))]
[assembly: Conversion(typeof(DictOfFloatSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatSbyteNullable))]
[assembly: Conversion(typeof(DictOfFloatSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatShort))]
[assembly: Conversion(typeof(DictOfFloatShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatShortNullable))]
[assembly: Conversion(typeof(DictOfFloatShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatInt))]
[assembly: Conversion(typeof(DictOfFloatIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatIntNullable))]
[assembly: Conversion(typeof(DictOfFloatIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatLong))]
[assembly: Conversion(typeof(DictOfFloatLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatLongNullable))]
[assembly: Conversion(typeof(DictOfFloatLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatFloat))]
[assembly: Conversion(typeof(DictOfFloatFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatFloatNullable))]
[assembly: Conversion(typeof(DictOfFloatFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatDouble))]
[assembly: Conversion(typeof(DictOfFloatDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatDoubleNullable))]
[assembly: Conversion(typeof(DictOfFloatDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatChar))]
[assembly: Conversion(typeof(DictOfFloatCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatCharNullable))]
[assembly: Conversion(typeof(DictOfFloatCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatString))]
[assembly: Conversion(typeof(DictOfFloatStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatDateTime))]
[assembly: Conversion(typeof(DictOfFloatDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatDateTimeNullable))]
[assembly: Conversion(typeof(DictOfFloatDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatGuid))]
[assembly: Conversion(typeof(DictOfFloatGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatGuidNullable))]
[assembly: Conversion(typeof(DictOfFloatGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatDecimal))]
[assembly: Conversion(typeof(DictOfFloatDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatDecimalNullable))]
[assembly: Conversion(typeof(DictOfFloatDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfFloatCompressedString))]
[assembly: Conversion(typeof(DictOfFloatCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleBool))]
[assembly: Conversion(typeof(DictOfDoubleBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleBoolNullable))]
[assembly: Conversion(typeof(DictOfDoubleBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleSbyte))]
[assembly: Conversion(typeof(DictOfDoubleSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleSbyteNullable))]
[assembly: Conversion(typeof(DictOfDoubleSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleShort))]
[assembly: Conversion(typeof(DictOfDoubleShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleShortNullable))]
[assembly: Conversion(typeof(DictOfDoubleShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleInt))]
[assembly: Conversion(typeof(DictOfDoubleIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleIntNullable))]
[assembly: Conversion(typeof(DictOfDoubleIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleLong))]
[assembly: Conversion(typeof(DictOfDoubleLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleLongNullable))]
[assembly: Conversion(typeof(DictOfDoubleLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleFloat))]
[assembly: Conversion(typeof(DictOfDoubleFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleFloatNullable))]
[assembly: Conversion(typeof(DictOfDoubleFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleDouble))]
[assembly: Conversion(typeof(DictOfDoubleDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleDoubleNullable))]
[assembly: Conversion(typeof(DictOfDoubleDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleChar))]
[assembly: Conversion(typeof(DictOfDoubleCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleCharNullable))]
[assembly: Conversion(typeof(DictOfDoubleCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleString))]
[assembly: Conversion(typeof(DictOfDoubleStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleDateTime))]
[assembly: Conversion(typeof(DictOfDoubleDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleDateTimeNullable))]
[assembly: Conversion(typeof(DictOfDoubleDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleGuid))]
[assembly: Conversion(typeof(DictOfDoubleGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleGuidNullable))]
[assembly: Conversion(typeof(DictOfDoubleGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleDecimal))]
[assembly: Conversion(typeof(DictOfDoubleDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleDecimalNullable))]
[assembly: Conversion(typeof(DictOfDoubleDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDoubleCompressedString))]
[assembly: Conversion(typeof(DictOfDoubleCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharBool))]
[assembly: Conversion(typeof(DictOfCharBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharBoolNullable))]
[assembly: Conversion(typeof(DictOfCharBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharSbyte))]
[assembly: Conversion(typeof(DictOfCharSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharSbyteNullable))]
[assembly: Conversion(typeof(DictOfCharSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharShort))]
[assembly: Conversion(typeof(DictOfCharShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharShortNullable))]
[assembly: Conversion(typeof(DictOfCharShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharInt))]
[assembly: Conversion(typeof(DictOfCharIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharIntNullable))]
[assembly: Conversion(typeof(DictOfCharIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharLong))]
[assembly: Conversion(typeof(DictOfCharLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharLongNullable))]
[assembly: Conversion(typeof(DictOfCharLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharFloat))]
[assembly: Conversion(typeof(DictOfCharFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharFloatNullable))]
[assembly: Conversion(typeof(DictOfCharFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharDouble))]
[assembly: Conversion(typeof(DictOfCharDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharDoubleNullable))]
[assembly: Conversion(typeof(DictOfCharDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharChar))]
[assembly: Conversion(typeof(DictOfCharCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharCharNullable))]
[assembly: Conversion(typeof(DictOfCharCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharString))]
[assembly: Conversion(typeof(DictOfCharStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharDateTime))]
[assembly: Conversion(typeof(DictOfCharDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharDateTimeNullable))]
[assembly: Conversion(typeof(DictOfCharDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharGuid))]
[assembly: Conversion(typeof(DictOfCharGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharGuidNullable))]
[assembly: Conversion(typeof(DictOfCharGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharDecimal))]
[assembly: Conversion(typeof(DictOfCharDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharDecimalNullable))]
[assembly: Conversion(typeof(DictOfCharDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCharCompressedString))]
[assembly: Conversion(typeof(DictOfCharCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringBool))]
[assembly: Conversion(typeof(DictOfStringBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringBoolNullable))]
[assembly: Conversion(typeof(DictOfStringBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringSbyte))]
[assembly: Conversion(typeof(DictOfStringSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringSbyteNullable))]
[assembly: Conversion(typeof(DictOfStringSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringShort))]
[assembly: Conversion(typeof(DictOfStringShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringShortNullable))]
[assembly: Conversion(typeof(DictOfStringShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringInt))]
[assembly: Conversion(typeof(DictOfStringIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringIntNullable))]
[assembly: Conversion(typeof(DictOfStringIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringLong))]
[assembly: Conversion(typeof(DictOfStringLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringLongNullable))]
[assembly: Conversion(typeof(DictOfStringLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringFloat))]
[assembly: Conversion(typeof(DictOfStringFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringFloatNullable))]
[assembly: Conversion(typeof(DictOfStringFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringDouble))]
[assembly: Conversion(typeof(DictOfStringDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringDoubleNullable))]
[assembly: Conversion(typeof(DictOfStringDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringChar))]
[assembly: Conversion(typeof(DictOfStringCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringCharNullable))]
[assembly: Conversion(typeof(DictOfStringCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringString))]
[assembly: Conversion(typeof(DictOfStringStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringDateTime))]
[assembly: Conversion(typeof(DictOfStringDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringDateTimeNullable))]
[assembly: Conversion(typeof(DictOfStringDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringGuid))]
[assembly: Conversion(typeof(DictOfStringGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringGuidNullable))]
[assembly: Conversion(typeof(DictOfStringGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringDecimal))]
[assembly: Conversion(typeof(DictOfStringDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringDecimalNullable))]
[assembly: Conversion(typeof(DictOfStringDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfStringCompressedString))]
[assembly: Conversion(typeof(DictOfStringCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeBool))]
[assembly: Conversion(typeof(DictOfDateTimeBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeBoolNullable))]
[assembly: Conversion(typeof(DictOfDateTimeBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeSbyte))]
[assembly: Conversion(typeof(DictOfDateTimeSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeSbyteNullable))]
[assembly: Conversion(typeof(DictOfDateTimeSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeShort))]
[assembly: Conversion(typeof(DictOfDateTimeShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeShortNullable))]
[assembly: Conversion(typeof(DictOfDateTimeShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeInt))]
[assembly: Conversion(typeof(DictOfDateTimeIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeIntNullable))]
[assembly: Conversion(typeof(DictOfDateTimeIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeLong))]
[assembly: Conversion(typeof(DictOfDateTimeLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeLongNullable))]
[assembly: Conversion(typeof(DictOfDateTimeLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeFloat))]
[assembly: Conversion(typeof(DictOfDateTimeFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeFloatNullable))]
[assembly: Conversion(typeof(DictOfDateTimeFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeDouble))]
[assembly: Conversion(typeof(DictOfDateTimeDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeDoubleNullable))]
[assembly: Conversion(typeof(DictOfDateTimeDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeChar))]
[assembly: Conversion(typeof(DictOfDateTimeCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeCharNullable))]
[assembly: Conversion(typeof(DictOfDateTimeCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeString))]
[assembly: Conversion(typeof(DictOfDateTimeStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeDateTime))]
[assembly: Conversion(typeof(DictOfDateTimeDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeDateTimeNullable))]
[assembly: Conversion(typeof(DictOfDateTimeDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeGuid))]
[assembly: Conversion(typeof(DictOfDateTimeGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeGuidNullable))]
[assembly: Conversion(typeof(DictOfDateTimeGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeDecimal))]
[assembly: Conversion(typeof(DictOfDateTimeDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeDecimalNullable))]
[assembly: Conversion(typeof(DictOfDateTimeDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDateTimeCompressedString))]
[assembly: Conversion(typeof(DictOfDateTimeCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidBool))]
[assembly: Conversion(typeof(DictOfGuidBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidBoolNullable))]
[assembly: Conversion(typeof(DictOfGuidBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidSbyte))]
[assembly: Conversion(typeof(DictOfGuidSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidSbyteNullable))]
[assembly: Conversion(typeof(DictOfGuidSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidShort))]
[assembly: Conversion(typeof(DictOfGuidShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidShortNullable))]
[assembly: Conversion(typeof(DictOfGuidShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidInt))]
[assembly: Conversion(typeof(DictOfGuidIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidIntNullable))]
[assembly: Conversion(typeof(DictOfGuidIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidLong))]
[assembly: Conversion(typeof(DictOfGuidLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidLongNullable))]
[assembly: Conversion(typeof(DictOfGuidLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidFloat))]
[assembly: Conversion(typeof(DictOfGuidFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidFloatNullable))]
[assembly: Conversion(typeof(DictOfGuidFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidDouble))]
[assembly: Conversion(typeof(DictOfGuidDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidDoubleNullable))]
[assembly: Conversion(typeof(DictOfGuidDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidChar))]
[assembly: Conversion(typeof(DictOfGuidCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidCharNullable))]
[assembly: Conversion(typeof(DictOfGuidCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidString))]
[assembly: Conversion(typeof(DictOfGuidStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidDateTime))]
[assembly: Conversion(typeof(DictOfGuidDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidDateTimeNullable))]
[assembly: Conversion(typeof(DictOfGuidDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidGuid))]
[assembly: Conversion(typeof(DictOfGuidGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidGuidNullable))]
[assembly: Conversion(typeof(DictOfGuidGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidDecimal))]
[assembly: Conversion(typeof(DictOfGuidDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidDecimalNullable))]
[assembly: Conversion(typeof(DictOfGuidDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfGuidCompressedString))]
[assembly: Conversion(typeof(DictOfGuidCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalBool))]
[assembly: Conversion(typeof(DictOfDecimalBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalBoolNullable))]
[assembly: Conversion(typeof(DictOfDecimalBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalSbyte))]
[assembly: Conversion(typeof(DictOfDecimalSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalSbyteNullable))]
[assembly: Conversion(typeof(DictOfDecimalSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalShort))]
[assembly: Conversion(typeof(DictOfDecimalShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalShortNullable))]
[assembly: Conversion(typeof(DictOfDecimalShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalInt))]
[assembly: Conversion(typeof(DictOfDecimalIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalIntNullable))]
[assembly: Conversion(typeof(DictOfDecimalIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalLong))]
[assembly: Conversion(typeof(DictOfDecimalLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalLongNullable))]
[assembly: Conversion(typeof(DictOfDecimalLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalFloat))]
[assembly: Conversion(typeof(DictOfDecimalFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalFloatNullable))]
[assembly: Conversion(typeof(DictOfDecimalFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalDouble))]
[assembly: Conversion(typeof(DictOfDecimalDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalDoubleNullable))]
[assembly: Conversion(typeof(DictOfDecimalDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalChar))]
[assembly: Conversion(typeof(DictOfDecimalCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalCharNullable))]
[assembly: Conversion(typeof(DictOfDecimalCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalString))]
[assembly: Conversion(typeof(DictOfDecimalStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalDateTime))]
[assembly: Conversion(typeof(DictOfDecimalDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalDateTimeNullable))]
[assembly: Conversion(typeof(DictOfDecimalDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalGuid))]
[assembly: Conversion(typeof(DictOfDecimalGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalGuidNullable))]
[assembly: Conversion(typeof(DictOfDecimalGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalDecimal))]
[assembly: Conversion(typeof(DictOfDecimalDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalDecimalNullable))]
[assembly: Conversion(typeof(DictOfDecimalDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfDecimalCompressedString))]
[assembly: Conversion(typeof(DictOfDecimalCompressedStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringBool))]
[assembly: Conversion(typeof(DictOfCompressedStringBoolToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringBoolNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringBoolNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringSbyte))]
[assembly: Conversion(typeof(DictOfCompressedStringSbyteToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringSbyteNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringSbyteNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringShort))]
[assembly: Conversion(typeof(DictOfCompressedStringShortToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringShortNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringShortNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringInt))]
[assembly: Conversion(typeof(DictOfCompressedStringIntToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringIntNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringIntNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringLong))]
[assembly: Conversion(typeof(DictOfCompressedStringLongToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringLongNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringLongNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringFloat))]
[assembly: Conversion(typeof(DictOfCompressedStringFloatToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringFloatNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringFloatNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringDouble))]
[assembly: Conversion(typeof(DictOfCompressedStringDoubleToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringDoubleNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringDoubleNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringChar))]
[assembly: Conversion(typeof(DictOfCompressedStringCharToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringCharNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringCharNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringString))]
[assembly: Conversion(typeof(DictOfCompressedStringStringToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringDateTime))]
[assembly: Conversion(typeof(DictOfCompressedStringDateTimeToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringDateTimeNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringDateTimeNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringGuid))]
[assembly: Conversion(typeof(DictOfCompressedStringGuidToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringGuidNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringGuidNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringDecimal))]
[assembly: Conversion(typeof(DictOfCompressedStringDecimalToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringDecimalNullable))]
[assembly: Conversion(typeof(DictOfCompressedStringDecimalNullableToDictOfObjectObject))]
[assembly: Conversion(typeof(DictOfObjectObjectToDictOfCompressedStringCompressedString))]
[assembly: Conversion(typeof(DictOfCompressedStringCompressedStringToDictOfObjectObject))]

#endregion

namespace Blueprint41.TypeConversion
{
	#region List<T>

	internal class ListOfObjectToListOfBool : Conversion<List<object>?, List<bool>?>
	{
		protected override List<bool>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<bool, bool>.Convert((bool)item)!).ToList();
		}
	}	
	internal class ListOfBoolToListOfObject : Conversion<List<bool>?, List<object>?>
	{
		protected override List<object>? Converter(List<bool>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<bool, bool>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfBoolNullable : Conversion<List<object?>?, List<bool?>?>
	{
		protected override List<bool?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<bool?, bool?>.Convert((bool?)item)!).ToList();
		}
	}	
	internal class ListOfBoolNullableToListOfObject : Conversion<List<bool?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<bool?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<bool?, bool?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfSbyte : Conversion<List<object>?, List<sbyte>?>
	{
		protected override List<sbyte>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long, sbyte>.Convert((long)item)!).ToList();
		}
	}	
	internal class ListOfSbyteToListOfObject : Conversion<List<sbyte>?, List<object>?>
	{
		protected override List<object>? Converter(List<sbyte>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<sbyte, long>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfSbyteNullable : Conversion<List<object?>?, List<sbyte?>?>
	{
		protected override List<sbyte?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long?, sbyte?>.Convert((long?)item)!).ToList();
		}
	}	
	internal class ListOfSbyteNullableToListOfObject : Conversion<List<sbyte?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<sbyte?, long?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfShort : Conversion<List<object>?, List<short>?>
	{
		protected override List<short>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long, short>.Convert((long)item)!).ToList();
		}
	}	
	internal class ListOfShortToListOfObject : Conversion<List<short>?, List<object>?>
	{
		protected override List<object>? Converter(List<short>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<short, long>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfShortNullable : Conversion<List<object?>?, List<short?>?>
	{
		protected override List<short?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long?, short?>.Convert((long?)item)!).ToList();
		}
	}	
	internal class ListOfShortNullableToListOfObject : Conversion<List<short?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<short?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<short?, long?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfInt : Conversion<List<object>?, List<int>?>
	{
		protected override List<int>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long, int>.Convert((long)item)!).ToList();
		}
	}	
	internal class ListOfIntToListOfObject : Conversion<List<int>?, List<object>?>
	{
		protected override List<object>? Converter(List<int>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<int, long>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfIntNullable : Conversion<List<object?>?, List<int?>?>
	{
		protected override List<int?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long?, int?>.Convert((long?)item)!).ToList();
		}
	}	
	internal class ListOfIntNullableToListOfObject : Conversion<List<int?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<int?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<int?, long?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfLong : Conversion<List<object>?, List<long>?>
	{
		protected override List<long>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long, long>.Convert((long)item)!).ToList();
		}
	}	
	internal class ListOfLongToListOfObject : Conversion<List<long>?, List<object>?>
	{
		protected override List<object>? Converter(List<long>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<long, long>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfLongNullable : Conversion<List<object?>?, List<long?>?>
	{
		protected override List<long?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long?, long?>.Convert((long?)item)!).ToList();
		}
	}	
	internal class ListOfLongNullableToListOfObject : Conversion<List<long?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<long?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<long?, long?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfFloat : Conversion<List<object>?, List<float>?>
	{
		protected override List<float>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<double, float>.Convert((double)item)!).ToList();
		}
	}	
	internal class ListOfFloatToListOfObject : Conversion<List<float>?, List<object>?>
	{
		protected override List<object>? Converter(List<float>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<float, double>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfFloatNullable : Conversion<List<object?>?, List<float?>?>
	{
		protected override List<float?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<double?, float?>.Convert((double?)item)!).ToList();
		}
	}	
	internal class ListOfFloatNullableToListOfObject : Conversion<List<float?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<float?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<float?, double?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfDouble : Conversion<List<object>?, List<double>?>
	{
		protected override List<double>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<double, double>.Convert((double)item)!).ToList();
		}
	}	
	internal class ListOfDoubleToListOfObject : Conversion<List<double>?, List<object>?>
	{
		protected override List<object>? Converter(List<double>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<double, double>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfDoubleNullable : Conversion<List<object?>?, List<double?>?>
	{
		protected override List<double?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<double?, double?>.Convert((double?)item)!).ToList();
		}
	}	
	internal class ListOfDoubleNullableToListOfObject : Conversion<List<double?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<double?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<double?, double?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfChar : Conversion<List<object>?, List<char>?>
	{
		protected override List<char>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<string, char>.Convert((string)item)!).ToList();
		}
	}	
	internal class ListOfCharToListOfObject : Conversion<List<char>?, List<object>?>
	{
		protected override List<object>? Converter(List<char>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<char, string>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfCharNullable : Conversion<List<object?>?, List<char?>?>
	{
		protected override List<char?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<string?, char?>.Convert((string?)item)).ToList();
		}
	}	
	internal class ListOfCharNullableToListOfObject : Conversion<List<char?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<char?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<char?, string>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfString : Conversion<List<object?>?, List<string?>?>
	{
		protected override List<string?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<string?, string>.Convert((string?)item)).ToList();
		}
	}	
	internal class ListOfStringToListOfObject : Conversion<List<string?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<string?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<string, string>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfDateTime : Conversion<List<object>?, List<DateTime>?>
	{
		protected override List<DateTime>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long, DateTime>.Convert((long)item)!).ToList();
		}
	}	
	internal class ListOfDateTimeToListOfObject : Conversion<List<DateTime>?, List<object>?>
	{
		protected override List<object>? Converter(List<DateTime>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<DateTime, long>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfDateTimeNullable : Conversion<List<object?>?, List<DateTime?>?>
	{
		protected override List<DateTime?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long?, DateTime?>.Convert((long?)item)!).ToList();
		}
	}	
	internal class ListOfDateTimeNullableToListOfObject : Conversion<List<DateTime?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<DateTime?, long?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfGuid : Conversion<List<object>?, List<Guid>?>
	{
		protected override List<Guid>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<string, Guid>.Convert((string)item)!).ToList();
		}
	}	
	internal class ListOfGuidToListOfObject : Conversion<List<Guid>?, List<object>?>
	{
		protected override List<object>? Converter(List<Guid>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<Guid, string>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfGuidNullable : Conversion<List<object?>?, List<Guid?>?>
	{
		protected override List<Guid?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<string?, Guid?>.Convert((string?)item)).ToList();
		}
	}	
	internal class ListOfGuidNullableToListOfObject : Conversion<List<Guid?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<Guid?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<Guid?, string>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfDecimal : Conversion<List<object>?, List<decimal>?>
	{
		protected override List<decimal>? Converter(List<object>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long, decimal>.Convert((long)item)!).ToList();
		}
	}	
	internal class ListOfDecimalToListOfObject : Conversion<List<decimal>?, List<object>?>
	{
		protected override List<object>? Converter(List<decimal>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object)Conversion<decimal, long>.Convert(item)!).ToList();
		}
	}	
	internal class ListOfObjectToListOfDecimalNullable : Conversion<List<object?>?, List<decimal?>?>
	{
		protected override List<decimal?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<long?, decimal?>.Convert((long?)item)!).ToList();
		}
	}	
	internal class ListOfDecimalNullableToListOfObject : Conversion<List<decimal?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<decimal?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<decimal?, long?>.Convert(item)).ToList();
		}
	}	
	internal class ListOfObjectToListOfCompressedString : Conversion<List<object?>?, List<CompressedString?>?>
	{
		protected override List<CompressedString?>? Converter(List<object?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => Conversion<byte[]?, CompressedString>.Convert((byte[]?)item)).ToList();
		}
	}	
	internal class ListOfCompressedStringToListOfObject : Conversion<List<CompressedString?>?, List<object?>?>
	{
		protected override List<object?>? Converter(List<CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.Select(item => (object?)Conversion<CompressedString, byte[]>.Convert(item)).ToList();
		}
	}	
		  
	#endregion

	#region Dictionary<TKey, TValue>

	internal class DictOfObjectObjectToDictOfBoolBool : Conversion<string?, Dictionary<bool, bool>?>
	{
		protected override Dictionary<bool, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, bool>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfBoolBoolToDictOfObjectObject : Conversion<Dictionary<bool, bool>?, string?>
	{
		protected override string? Converter(Dictionary<bool, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolBoolNullable : Conversion<string?, Dictionary<bool, bool?>?>
	{
		protected override Dictionary<bool, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, bool?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfBoolBoolNullableToDictOfObjectObject : Conversion<Dictionary<bool, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolSbyte : Conversion<string?, Dictionary<bool, sbyte>?>
	{
		protected override Dictionary<bool, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfBoolSbyteToDictOfObjectObject : Conversion<Dictionary<bool, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<bool, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolSbyteNullable : Conversion<string?, Dictionary<bool, sbyte?>?>
	{
		protected override Dictionary<bool, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfBoolSbyteNullableToDictOfObjectObject : Conversion<Dictionary<bool, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolShort : Conversion<string?, Dictionary<bool, short>?>
	{
		protected override Dictionary<bool, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfBoolShortToDictOfObjectObject : Conversion<Dictionary<bool, short>?, string?>
	{
		protected override string? Converter(Dictionary<bool, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolShortNullable : Conversion<string?, Dictionary<bool, short?>?>
	{
		protected override Dictionary<bool, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfBoolShortNullableToDictOfObjectObject : Conversion<Dictionary<bool, short?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolInt : Conversion<string?, Dictionary<bool, int>?>
	{
		protected override Dictionary<bool, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfBoolIntToDictOfObjectObject : Conversion<Dictionary<bool, int>?, string?>
	{
		protected override string? Converter(Dictionary<bool, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolIntNullable : Conversion<string?, Dictionary<bool, int?>?>
	{
		protected override Dictionary<bool, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfBoolIntNullableToDictOfObjectObject : Conversion<Dictionary<bool, int?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolLong : Conversion<string?, Dictionary<bool, long>?>
	{
		protected override Dictionary<bool, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfBoolLongToDictOfObjectObject : Conversion<Dictionary<bool, long>?, string?>
	{
		protected override string? Converter(Dictionary<bool, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolLongNullable : Conversion<string?, Dictionary<bool, long?>?>
	{
		protected override Dictionary<bool, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfBoolLongNullableToDictOfObjectObject : Conversion<Dictionary<bool, long?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolFloat : Conversion<string?, Dictionary<bool, float>?>
	{
		protected override Dictionary<bool, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, double>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfBoolFloatToDictOfObjectObject : Conversion<Dictionary<bool, float>?, string?>
	{
		protected override string? Converter(Dictionary<bool, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolFloatNullable : Conversion<string?, Dictionary<bool, float?>?>
	{
		protected override Dictionary<bool, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, double?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfBoolFloatNullableToDictOfObjectObject : Conversion<Dictionary<bool, float?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolDouble : Conversion<string?, Dictionary<bool, double>?>
	{
		protected override Dictionary<bool, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, double>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfBoolDoubleToDictOfObjectObject : Conversion<Dictionary<bool, double>?, string?>
	{
		protected override string? Converter(Dictionary<bool, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolDoubleNullable : Conversion<string?, Dictionary<bool, double?>?>
	{
		protected override Dictionary<bool, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, double?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfBoolDoubleNullableToDictOfObjectObject : Conversion<Dictionary<bool, double?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolChar : Conversion<string?, Dictionary<bool, char>?>
	{
		protected override Dictionary<bool, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, string>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfBoolCharToDictOfObjectObject : Conversion<Dictionary<bool, char>?, string?>
	{
		protected override string? Converter(Dictionary<bool, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolCharNullable : Conversion<string?, Dictionary<bool, char?>?>
	{
		protected override Dictionary<bool, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, string>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfBoolCharNullableToDictOfObjectObject : Conversion<Dictionary<bool, char?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolString : Conversion<string?, Dictionary<bool, string?>?>
	{
		protected override Dictionary<bool, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, string>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfBoolStringToDictOfObjectObject : Conversion<Dictionary<bool, string?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolDateTime : Conversion<string?, Dictionary<bool, DateTime>?>
	{
		protected override Dictionary<bool, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfBoolDateTimeToDictOfObjectObject : Conversion<Dictionary<bool, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<bool, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolDateTimeNullable : Conversion<string?, Dictionary<bool, DateTime?>?>
	{
		protected override Dictionary<bool, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfBoolDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<bool, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolGuid : Conversion<string?, Dictionary<bool, Guid>?>
	{
		protected override Dictionary<bool, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, string>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfBoolGuidToDictOfObjectObject : Conversion<Dictionary<bool, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<bool, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolGuidNullable : Conversion<string?, Dictionary<bool, Guid?>?>
	{
		protected override Dictionary<bool, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, string>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfBoolGuidNullableToDictOfObjectObject : Conversion<Dictionary<bool, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolDecimal : Conversion<string?, Dictionary<bool, decimal>?>
	{
		protected override Dictionary<bool, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfBoolDecimalToDictOfObjectObject : Conversion<Dictionary<bool, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<bool, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolDecimalNullable : Conversion<string?, Dictionary<bool, decimal?>?>
	{
		protected override Dictionary<bool, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, long?>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfBoolDecimalNullableToDictOfObjectObject : Conversion<Dictionary<bool, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfBoolCompressedString : Conversion<string?, Dictionary<bool, CompressedString?>?>
	{
		protected override Dictionary<bool, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<bool, byte[]>>().ToDictionary(item => Conversion<bool, bool>.Convert((bool)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfBoolCompressedStringToDictOfObjectObject : Conversion<Dictionary<bool, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<bool, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<bool, bool>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteBool : Conversion<string?, Dictionary<sbyte, bool>?>
	{
		protected override Dictionary<sbyte, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfSbyteBoolToDictOfObjectObject : Conversion<Dictionary<sbyte, bool>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteBoolNullable : Conversion<string?, Dictionary<sbyte, bool?>?>
	{
		protected override Dictionary<sbyte, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfSbyteBoolNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteSbyte : Conversion<string?, Dictionary<sbyte, sbyte>?>
	{
		protected override Dictionary<sbyte, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfSbyteSbyteToDictOfObjectObject : Conversion<Dictionary<sbyte, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteSbyteNullable : Conversion<string?, Dictionary<sbyte, sbyte?>?>
	{
		protected override Dictionary<sbyte, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfSbyteSbyteNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteShort : Conversion<string?, Dictionary<sbyte, short>?>
	{
		protected override Dictionary<sbyte, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfSbyteShortToDictOfObjectObject : Conversion<Dictionary<sbyte, short>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteShortNullable : Conversion<string?, Dictionary<sbyte, short?>?>
	{
		protected override Dictionary<sbyte, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfSbyteShortNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, short?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteInt : Conversion<string?, Dictionary<sbyte, int>?>
	{
		protected override Dictionary<sbyte, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfSbyteIntToDictOfObjectObject : Conversion<Dictionary<sbyte, int>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteIntNullable : Conversion<string?, Dictionary<sbyte, int?>?>
	{
		protected override Dictionary<sbyte, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfSbyteIntNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, int?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteLong : Conversion<string?, Dictionary<sbyte, long>?>
	{
		protected override Dictionary<sbyte, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfSbyteLongToDictOfObjectObject : Conversion<Dictionary<sbyte, long>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteLongNullable : Conversion<string?, Dictionary<sbyte, long?>?>
	{
		protected override Dictionary<sbyte, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfSbyteLongNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, long?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteFloat : Conversion<string?, Dictionary<sbyte, float>?>
	{
		protected override Dictionary<sbyte, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfSbyteFloatToDictOfObjectObject : Conversion<Dictionary<sbyte, float>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteFloatNullable : Conversion<string?, Dictionary<sbyte, float?>?>
	{
		protected override Dictionary<sbyte, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfSbyteFloatNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, float?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteDouble : Conversion<string?, Dictionary<sbyte, double>?>
	{
		protected override Dictionary<sbyte, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfSbyteDoubleToDictOfObjectObject : Conversion<Dictionary<sbyte, double>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteDoubleNullable : Conversion<string?, Dictionary<sbyte, double?>?>
	{
		protected override Dictionary<sbyte, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfSbyteDoubleNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, double?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteChar : Conversion<string?, Dictionary<sbyte, char>?>
	{
		protected override Dictionary<sbyte, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfSbyteCharToDictOfObjectObject : Conversion<Dictionary<sbyte, char>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteCharNullable : Conversion<string?, Dictionary<sbyte, char?>?>
	{
		protected override Dictionary<sbyte, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfSbyteCharNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, char?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteString : Conversion<string?, Dictionary<sbyte, string?>?>
	{
		protected override Dictionary<sbyte, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfSbyteStringToDictOfObjectObject : Conversion<Dictionary<sbyte, string?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteDateTime : Conversion<string?, Dictionary<sbyte, DateTime>?>
	{
		protected override Dictionary<sbyte, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfSbyteDateTimeToDictOfObjectObject : Conversion<Dictionary<sbyte, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteDateTimeNullable : Conversion<string?, Dictionary<sbyte, DateTime?>?>
	{
		protected override Dictionary<sbyte, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfSbyteDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteGuid : Conversion<string?, Dictionary<sbyte, Guid>?>
	{
		protected override Dictionary<sbyte, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfSbyteGuidToDictOfObjectObject : Conversion<Dictionary<sbyte, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteGuidNullable : Conversion<string?, Dictionary<sbyte, Guid?>?>
	{
		protected override Dictionary<sbyte, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfSbyteGuidNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteDecimal : Conversion<string?, Dictionary<sbyte, decimal>?>
	{
		protected override Dictionary<sbyte, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfSbyteDecimalToDictOfObjectObject : Conversion<Dictionary<sbyte, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteDecimalNullable : Conversion<string?, Dictionary<sbyte, decimal?>?>
	{
		protected override Dictionary<sbyte, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfSbyteDecimalNullableToDictOfObjectObject : Conversion<Dictionary<sbyte, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfSbyteCompressedString : Conversion<string?, Dictionary<sbyte, CompressedString?>?>
	{
		protected override Dictionary<sbyte, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, byte[]>>().ToDictionary(item => Conversion<long, sbyte>.Convert((long)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfSbyteCompressedStringToDictOfObjectObject : Conversion<Dictionary<sbyte, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<sbyte, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<sbyte, long>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortBool : Conversion<string?, Dictionary<short, bool>?>
	{
		protected override Dictionary<short, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfShortBoolToDictOfObjectObject : Conversion<Dictionary<short, bool>?, string?>
	{
		protected override string? Converter(Dictionary<short, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortBoolNullable : Conversion<string?, Dictionary<short, bool?>?>
	{
		protected override Dictionary<short, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfShortBoolNullableToDictOfObjectObject : Conversion<Dictionary<short, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<short, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortSbyte : Conversion<string?, Dictionary<short, sbyte>?>
	{
		protected override Dictionary<short, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfShortSbyteToDictOfObjectObject : Conversion<Dictionary<short, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<short, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortSbyteNullable : Conversion<string?, Dictionary<short, sbyte?>?>
	{
		protected override Dictionary<short, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfShortSbyteNullableToDictOfObjectObject : Conversion<Dictionary<short, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<short, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortShort : Conversion<string?, Dictionary<short, short>?>
	{
		protected override Dictionary<short, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfShortShortToDictOfObjectObject : Conversion<Dictionary<short, short>?, string?>
	{
		protected override string? Converter(Dictionary<short, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortShortNullable : Conversion<string?, Dictionary<short, short?>?>
	{
		protected override Dictionary<short, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfShortShortNullableToDictOfObjectObject : Conversion<Dictionary<short, short?>?, string?>
	{
		protected override string? Converter(Dictionary<short, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortInt : Conversion<string?, Dictionary<short, int>?>
	{
		protected override Dictionary<short, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfShortIntToDictOfObjectObject : Conversion<Dictionary<short, int>?, string?>
	{
		protected override string? Converter(Dictionary<short, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortIntNullable : Conversion<string?, Dictionary<short, int?>?>
	{
		protected override Dictionary<short, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfShortIntNullableToDictOfObjectObject : Conversion<Dictionary<short, int?>?, string?>
	{
		protected override string? Converter(Dictionary<short, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortLong : Conversion<string?, Dictionary<short, long>?>
	{
		protected override Dictionary<short, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfShortLongToDictOfObjectObject : Conversion<Dictionary<short, long>?, string?>
	{
		protected override string? Converter(Dictionary<short, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortLongNullable : Conversion<string?, Dictionary<short, long?>?>
	{
		protected override Dictionary<short, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfShortLongNullableToDictOfObjectObject : Conversion<Dictionary<short, long?>?, string?>
	{
		protected override string? Converter(Dictionary<short, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortFloat : Conversion<string?, Dictionary<short, float>?>
	{
		protected override Dictionary<short, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfShortFloatToDictOfObjectObject : Conversion<Dictionary<short, float>?, string?>
	{
		protected override string? Converter(Dictionary<short, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortFloatNullable : Conversion<string?, Dictionary<short, float?>?>
	{
		protected override Dictionary<short, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfShortFloatNullableToDictOfObjectObject : Conversion<Dictionary<short, float?>?, string?>
	{
		protected override string? Converter(Dictionary<short, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortDouble : Conversion<string?, Dictionary<short, double>?>
	{
		protected override Dictionary<short, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfShortDoubleToDictOfObjectObject : Conversion<Dictionary<short, double>?, string?>
	{
		protected override string? Converter(Dictionary<short, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortDoubleNullable : Conversion<string?, Dictionary<short, double?>?>
	{
		protected override Dictionary<short, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfShortDoubleNullableToDictOfObjectObject : Conversion<Dictionary<short, double?>?, string?>
	{
		protected override string? Converter(Dictionary<short, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortChar : Conversion<string?, Dictionary<short, char>?>
	{
		protected override Dictionary<short, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfShortCharToDictOfObjectObject : Conversion<Dictionary<short, char>?, string?>
	{
		protected override string? Converter(Dictionary<short, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortCharNullable : Conversion<string?, Dictionary<short, char?>?>
	{
		protected override Dictionary<short, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfShortCharNullableToDictOfObjectObject : Conversion<Dictionary<short, char?>?, string?>
	{
		protected override string? Converter(Dictionary<short, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortString : Conversion<string?, Dictionary<short, string?>?>
	{
		protected override Dictionary<short, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfShortStringToDictOfObjectObject : Conversion<Dictionary<short, string?>?, string?>
	{
		protected override string? Converter(Dictionary<short, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortDateTime : Conversion<string?, Dictionary<short, DateTime>?>
	{
		protected override Dictionary<short, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfShortDateTimeToDictOfObjectObject : Conversion<Dictionary<short, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<short, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortDateTimeNullable : Conversion<string?, Dictionary<short, DateTime?>?>
	{
		protected override Dictionary<short, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfShortDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<short, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<short, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortGuid : Conversion<string?, Dictionary<short, Guid>?>
	{
		protected override Dictionary<short, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfShortGuidToDictOfObjectObject : Conversion<Dictionary<short, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<short, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortGuidNullable : Conversion<string?, Dictionary<short, Guid?>?>
	{
		protected override Dictionary<short, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfShortGuidNullableToDictOfObjectObject : Conversion<Dictionary<short, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<short, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortDecimal : Conversion<string?, Dictionary<short, decimal>?>
	{
		protected override Dictionary<short, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfShortDecimalToDictOfObjectObject : Conversion<Dictionary<short, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<short, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortDecimalNullable : Conversion<string?, Dictionary<short, decimal?>?>
	{
		protected override Dictionary<short, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfShortDecimalNullableToDictOfObjectObject : Conversion<Dictionary<short, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<short, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfShortCompressedString : Conversion<string?, Dictionary<short, CompressedString?>?>
	{
		protected override Dictionary<short, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, byte[]>>().ToDictionary(item => Conversion<long, short>.Convert((long)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfShortCompressedStringToDictOfObjectObject : Conversion<Dictionary<short, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<short, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<short, long>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntBool : Conversion<string?, Dictionary<int, bool>?>
	{
		protected override Dictionary<int, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfIntBoolToDictOfObjectObject : Conversion<Dictionary<int, bool>?, string?>
	{
		protected override string? Converter(Dictionary<int, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntBoolNullable : Conversion<string?, Dictionary<int, bool?>?>
	{
		protected override Dictionary<int, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfIntBoolNullableToDictOfObjectObject : Conversion<Dictionary<int, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<int, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntSbyte : Conversion<string?, Dictionary<int, sbyte>?>
	{
		protected override Dictionary<int, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfIntSbyteToDictOfObjectObject : Conversion<Dictionary<int, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<int, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntSbyteNullable : Conversion<string?, Dictionary<int, sbyte?>?>
	{
		protected override Dictionary<int, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfIntSbyteNullableToDictOfObjectObject : Conversion<Dictionary<int, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<int, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntShort : Conversion<string?, Dictionary<int, short>?>
	{
		protected override Dictionary<int, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfIntShortToDictOfObjectObject : Conversion<Dictionary<int, short>?, string?>
	{
		protected override string? Converter(Dictionary<int, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntShortNullable : Conversion<string?, Dictionary<int, short?>?>
	{
		protected override Dictionary<int, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfIntShortNullableToDictOfObjectObject : Conversion<Dictionary<int, short?>?, string?>
	{
		protected override string? Converter(Dictionary<int, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntInt : Conversion<string?, Dictionary<int, int>?>
	{
		protected override Dictionary<int, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfIntIntToDictOfObjectObject : Conversion<Dictionary<int, int>?, string?>
	{
		protected override string? Converter(Dictionary<int, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntIntNullable : Conversion<string?, Dictionary<int, int?>?>
	{
		protected override Dictionary<int, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfIntIntNullableToDictOfObjectObject : Conversion<Dictionary<int, int?>?, string?>
	{
		protected override string? Converter(Dictionary<int, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntLong : Conversion<string?, Dictionary<int, long>?>
	{
		protected override Dictionary<int, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfIntLongToDictOfObjectObject : Conversion<Dictionary<int, long>?, string?>
	{
		protected override string? Converter(Dictionary<int, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntLongNullable : Conversion<string?, Dictionary<int, long?>?>
	{
		protected override Dictionary<int, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfIntLongNullableToDictOfObjectObject : Conversion<Dictionary<int, long?>?, string?>
	{
		protected override string? Converter(Dictionary<int, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntFloat : Conversion<string?, Dictionary<int, float>?>
	{
		protected override Dictionary<int, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfIntFloatToDictOfObjectObject : Conversion<Dictionary<int, float>?, string?>
	{
		protected override string? Converter(Dictionary<int, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntFloatNullable : Conversion<string?, Dictionary<int, float?>?>
	{
		protected override Dictionary<int, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfIntFloatNullableToDictOfObjectObject : Conversion<Dictionary<int, float?>?, string?>
	{
		protected override string? Converter(Dictionary<int, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntDouble : Conversion<string?, Dictionary<int, double>?>
	{
		protected override Dictionary<int, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfIntDoubleToDictOfObjectObject : Conversion<Dictionary<int, double>?, string?>
	{
		protected override string? Converter(Dictionary<int, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntDoubleNullable : Conversion<string?, Dictionary<int, double?>?>
	{
		protected override Dictionary<int, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfIntDoubleNullableToDictOfObjectObject : Conversion<Dictionary<int, double?>?, string?>
	{
		protected override string? Converter(Dictionary<int, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntChar : Conversion<string?, Dictionary<int, char>?>
	{
		protected override Dictionary<int, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfIntCharToDictOfObjectObject : Conversion<Dictionary<int, char>?, string?>
	{
		protected override string? Converter(Dictionary<int, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntCharNullable : Conversion<string?, Dictionary<int, char?>?>
	{
		protected override Dictionary<int, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfIntCharNullableToDictOfObjectObject : Conversion<Dictionary<int, char?>?, string?>
	{
		protected override string? Converter(Dictionary<int, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntString : Conversion<string?, Dictionary<int, string?>?>
	{
		protected override Dictionary<int, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfIntStringToDictOfObjectObject : Conversion<Dictionary<int, string?>?, string?>
	{
		protected override string? Converter(Dictionary<int, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntDateTime : Conversion<string?, Dictionary<int, DateTime>?>
	{
		protected override Dictionary<int, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfIntDateTimeToDictOfObjectObject : Conversion<Dictionary<int, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<int, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntDateTimeNullable : Conversion<string?, Dictionary<int, DateTime?>?>
	{
		protected override Dictionary<int, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfIntDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<int, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<int, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntGuid : Conversion<string?, Dictionary<int, Guid>?>
	{
		protected override Dictionary<int, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfIntGuidToDictOfObjectObject : Conversion<Dictionary<int, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<int, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntGuidNullable : Conversion<string?, Dictionary<int, Guid?>?>
	{
		protected override Dictionary<int, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfIntGuidNullableToDictOfObjectObject : Conversion<Dictionary<int, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<int, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntDecimal : Conversion<string?, Dictionary<int, decimal>?>
	{
		protected override Dictionary<int, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfIntDecimalToDictOfObjectObject : Conversion<Dictionary<int, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<int, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntDecimalNullable : Conversion<string?, Dictionary<int, decimal?>?>
	{
		protected override Dictionary<int, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfIntDecimalNullableToDictOfObjectObject : Conversion<Dictionary<int, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<int, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfIntCompressedString : Conversion<string?, Dictionary<int, CompressedString?>?>
	{
		protected override Dictionary<int, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, byte[]>>().ToDictionary(item => Conversion<long, int>.Convert((long)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfIntCompressedStringToDictOfObjectObject : Conversion<Dictionary<int, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<int, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<int, long>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongBool : Conversion<string?, Dictionary<long, bool>?>
	{
		protected override Dictionary<long, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfLongBoolToDictOfObjectObject : Conversion<Dictionary<long, bool>?, string?>
	{
		protected override string? Converter(Dictionary<long, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongBoolNullable : Conversion<string?, Dictionary<long, bool?>?>
	{
		protected override Dictionary<long, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfLongBoolNullableToDictOfObjectObject : Conversion<Dictionary<long, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<long, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongSbyte : Conversion<string?, Dictionary<long, sbyte>?>
	{
		protected override Dictionary<long, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfLongSbyteToDictOfObjectObject : Conversion<Dictionary<long, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<long, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongSbyteNullable : Conversion<string?, Dictionary<long, sbyte?>?>
	{
		protected override Dictionary<long, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfLongSbyteNullableToDictOfObjectObject : Conversion<Dictionary<long, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<long, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongShort : Conversion<string?, Dictionary<long, short>?>
	{
		protected override Dictionary<long, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfLongShortToDictOfObjectObject : Conversion<Dictionary<long, short>?, string?>
	{
		protected override string? Converter(Dictionary<long, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongShortNullable : Conversion<string?, Dictionary<long, short?>?>
	{
		protected override Dictionary<long, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfLongShortNullableToDictOfObjectObject : Conversion<Dictionary<long, short?>?, string?>
	{
		protected override string? Converter(Dictionary<long, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongInt : Conversion<string?, Dictionary<long, int>?>
	{
		protected override Dictionary<long, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfLongIntToDictOfObjectObject : Conversion<Dictionary<long, int>?, string?>
	{
		protected override string? Converter(Dictionary<long, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongIntNullable : Conversion<string?, Dictionary<long, int?>?>
	{
		protected override Dictionary<long, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfLongIntNullableToDictOfObjectObject : Conversion<Dictionary<long, int?>?, string?>
	{
		protected override string? Converter(Dictionary<long, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongLong : Conversion<string?, Dictionary<long, long>?>
	{
		protected override Dictionary<long, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfLongLongToDictOfObjectObject : Conversion<Dictionary<long, long>?, string?>
	{
		protected override string? Converter(Dictionary<long, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongLongNullable : Conversion<string?, Dictionary<long, long?>?>
	{
		protected override Dictionary<long, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfLongLongNullableToDictOfObjectObject : Conversion<Dictionary<long, long?>?, string?>
	{
		protected override string? Converter(Dictionary<long, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongFloat : Conversion<string?, Dictionary<long, float>?>
	{
		protected override Dictionary<long, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfLongFloatToDictOfObjectObject : Conversion<Dictionary<long, float>?, string?>
	{
		protected override string? Converter(Dictionary<long, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongFloatNullable : Conversion<string?, Dictionary<long, float?>?>
	{
		protected override Dictionary<long, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfLongFloatNullableToDictOfObjectObject : Conversion<Dictionary<long, float?>?, string?>
	{
		protected override string? Converter(Dictionary<long, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongDouble : Conversion<string?, Dictionary<long, double>?>
	{
		protected override Dictionary<long, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfLongDoubleToDictOfObjectObject : Conversion<Dictionary<long, double>?, string?>
	{
		protected override string? Converter(Dictionary<long, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongDoubleNullable : Conversion<string?, Dictionary<long, double?>?>
	{
		protected override Dictionary<long, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfLongDoubleNullableToDictOfObjectObject : Conversion<Dictionary<long, double?>?, string?>
	{
		protected override string? Converter(Dictionary<long, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongChar : Conversion<string?, Dictionary<long, char>?>
	{
		protected override Dictionary<long, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfLongCharToDictOfObjectObject : Conversion<Dictionary<long, char>?, string?>
	{
		protected override string? Converter(Dictionary<long, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongCharNullable : Conversion<string?, Dictionary<long, char?>?>
	{
		protected override Dictionary<long, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfLongCharNullableToDictOfObjectObject : Conversion<Dictionary<long, char?>?, string?>
	{
		protected override string? Converter(Dictionary<long, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongString : Conversion<string?, Dictionary<long, string?>?>
	{
		protected override Dictionary<long, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfLongStringToDictOfObjectObject : Conversion<Dictionary<long, string?>?, string?>
	{
		protected override string? Converter(Dictionary<long, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongDateTime : Conversion<string?, Dictionary<long, DateTime>?>
	{
		protected override Dictionary<long, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfLongDateTimeToDictOfObjectObject : Conversion<Dictionary<long, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<long, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongDateTimeNullable : Conversion<string?, Dictionary<long, DateTime?>?>
	{
		protected override Dictionary<long, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfLongDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<long, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<long, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongGuid : Conversion<string?, Dictionary<long, Guid>?>
	{
		protected override Dictionary<long, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfLongGuidToDictOfObjectObject : Conversion<Dictionary<long, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<long, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongGuidNullable : Conversion<string?, Dictionary<long, Guid?>?>
	{
		protected override Dictionary<long, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfLongGuidNullableToDictOfObjectObject : Conversion<Dictionary<long, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<long, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongDecimal : Conversion<string?, Dictionary<long, decimal>?>
	{
		protected override Dictionary<long, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfLongDecimalToDictOfObjectObject : Conversion<Dictionary<long, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<long, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongDecimalNullable : Conversion<string?, Dictionary<long, decimal?>?>
	{
		protected override Dictionary<long, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfLongDecimalNullableToDictOfObjectObject : Conversion<Dictionary<long, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<long, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfLongCompressedString : Conversion<string?, Dictionary<long, CompressedString?>?>
	{
		protected override Dictionary<long, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, byte[]>>().ToDictionary(item => Conversion<long, long>.Convert((long)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfLongCompressedStringToDictOfObjectObject : Conversion<Dictionary<long, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<long, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<long, long>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatBool : Conversion<string?, Dictionary<float, bool>?>
	{
		protected override Dictionary<float, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, bool>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfFloatBoolToDictOfObjectObject : Conversion<Dictionary<float, bool>?, string?>
	{
		protected override string? Converter(Dictionary<float, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatBoolNullable : Conversion<string?, Dictionary<float, bool?>?>
	{
		protected override Dictionary<float, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, bool?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfFloatBoolNullableToDictOfObjectObject : Conversion<Dictionary<float, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<float, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatSbyte : Conversion<string?, Dictionary<float, sbyte>?>
	{
		protected override Dictionary<float, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfFloatSbyteToDictOfObjectObject : Conversion<Dictionary<float, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<float, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatSbyteNullable : Conversion<string?, Dictionary<float, sbyte?>?>
	{
		protected override Dictionary<float, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfFloatSbyteNullableToDictOfObjectObject : Conversion<Dictionary<float, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<float, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatShort : Conversion<string?, Dictionary<float, short>?>
	{
		protected override Dictionary<float, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfFloatShortToDictOfObjectObject : Conversion<Dictionary<float, short>?, string?>
	{
		protected override string? Converter(Dictionary<float, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatShortNullable : Conversion<string?, Dictionary<float, short?>?>
	{
		protected override Dictionary<float, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfFloatShortNullableToDictOfObjectObject : Conversion<Dictionary<float, short?>?, string?>
	{
		protected override string? Converter(Dictionary<float, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatInt : Conversion<string?, Dictionary<float, int>?>
	{
		protected override Dictionary<float, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfFloatIntToDictOfObjectObject : Conversion<Dictionary<float, int>?, string?>
	{
		protected override string? Converter(Dictionary<float, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatIntNullable : Conversion<string?, Dictionary<float, int?>?>
	{
		protected override Dictionary<float, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfFloatIntNullableToDictOfObjectObject : Conversion<Dictionary<float, int?>?, string?>
	{
		protected override string? Converter(Dictionary<float, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatLong : Conversion<string?, Dictionary<float, long>?>
	{
		protected override Dictionary<float, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfFloatLongToDictOfObjectObject : Conversion<Dictionary<float, long>?, string?>
	{
		protected override string? Converter(Dictionary<float, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatLongNullable : Conversion<string?, Dictionary<float, long?>?>
	{
		protected override Dictionary<float, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfFloatLongNullableToDictOfObjectObject : Conversion<Dictionary<float, long?>?, string?>
	{
		protected override string? Converter(Dictionary<float, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatFloat : Conversion<string?, Dictionary<float, float>?>
	{
		protected override Dictionary<float, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfFloatFloatToDictOfObjectObject : Conversion<Dictionary<float, float>?, string?>
	{
		protected override string? Converter(Dictionary<float, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatFloatNullable : Conversion<string?, Dictionary<float, float?>?>
	{
		protected override Dictionary<float, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfFloatFloatNullableToDictOfObjectObject : Conversion<Dictionary<float, float?>?, string?>
	{
		protected override string? Converter(Dictionary<float, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatDouble : Conversion<string?, Dictionary<float, double>?>
	{
		protected override Dictionary<float, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfFloatDoubleToDictOfObjectObject : Conversion<Dictionary<float, double>?, string?>
	{
		protected override string? Converter(Dictionary<float, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatDoubleNullable : Conversion<string?, Dictionary<float, double?>?>
	{
		protected override Dictionary<float, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfFloatDoubleNullableToDictOfObjectObject : Conversion<Dictionary<float, double?>?, string?>
	{
		protected override string? Converter(Dictionary<float, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatChar : Conversion<string?, Dictionary<float, char>?>
	{
		protected override Dictionary<float, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfFloatCharToDictOfObjectObject : Conversion<Dictionary<float, char>?, string?>
	{
		protected override string? Converter(Dictionary<float, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatCharNullable : Conversion<string?, Dictionary<float, char?>?>
	{
		protected override Dictionary<float, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfFloatCharNullableToDictOfObjectObject : Conversion<Dictionary<float, char?>?, string?>
	{
		protected override string? Converter(Dictionary<float, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatString : Conversion<string?, Dictionary<float, string?>?>
	{
		protected override Dictionary<float, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfFloatStringToDictOfObjectObject : Conversion<Dictionary<float, string?>?, string?>
	{
		protected override string? Converter(Dictionary<float, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatDateTime : Conversion<string?, Dictionary<float, DateTime>?>
	{
		protected override Dictionary<float, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfFloatDateTimeToDictOfObjectObject : Conversion<Dictionary<float, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<float, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatDateTimeNullable : Conversion<string?, Dictionary<float, DateTime?>?>
	{
		protected override Dictionary<float, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfFloatDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<float, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<float, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatGuid : Conversion<string?, Dictionary<float, Guid>?>
	{
		protected override Dictionary<float, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfFloatGuidToDictOfObjectObject : Conversion<Dictionary<float, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<float, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatGuidNullable : Conversion<string?, Dictionary<float, Guid?>?>
	{
		protected override Dictionary<float, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfFloatGuidNullableToDictOfObjectObject : Conversion<Dictionary<float, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<float, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatDecimal : Conversion<string?, Dictionary<float, decimal>?>
	{
		protected override Dictionary<float, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfFloatDecimalToDictOfObjectObject : Conversion<Dictionary<float, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<float, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatDecimalNullable : Conversion<string?, Dictionary<float, decimal?>?>
	{
		protected override Dictionary<float, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfFloatDecimalNullableToDictOfObjectObject : Conversion<Dictionary<float, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<float, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfFloatCompressedString : Conversion<string?, Dictionary<float, CompressedString?>?>
	{
		protected override Dictionary<float, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, byte[]>>().ToDictionary(item => Conversion<double, float>.Convert((double)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfFloatCompressedStringToDictOfObjectObject : Conversion<Dictionary<float, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<float, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<float, double>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleBool : Conversion<string?, Dictionary<double, bool>?>
	{
		protected override Dictionary<double, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, bool>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfDoubleBoolToDictOfObjectObject : Conversion<Dictionary<double, bool>?, string?>
	{
		protected override string? Converter(Dictionary<double, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleBoolNullable : Conversion<string?, Dictionary<double, bool?>?>
	{
		protected override Dictionary<double, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, bool?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfDoubleBoolNullableToDictOfObjectObject : Conversion<Dictionary<double, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<double, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleSbyte : Conversion<string?, Dictionary<double, sbyte>?>
	{
		protected override Dictionary<double, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDoubleSbyteToDictOfObjectObject : Conversion<Dictionary<double, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<double, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleSbyteNullable : Conversion<string?, Dictionary<double, sbyte?>?>
	{
		protected override Dictionary<double, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDoubleSbyteNullableToDictOfObjectObject : Conversion<Dictionary<double, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<double, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleShort : Conversion<string?, Dictionary<double, short>?>
	{
		protected override Dictionary<double, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDoubleShortToDictOfObjectObject : Conversion<Dictionary<double, short>?, string?>
	{
		protected override string? Converter(Dictionary<double, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleShortNullable : Conversion<string?, Dictionary<double, short?>?>
	{
		protected override Dictionary<double, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDoubleShortNullableToDictOfObjectObject : Conversion<Dictionary<double, short?>?, string?>
	{
		protected override string? Converter(Dictionary<double, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleInt : Conversion<string?, Dictionary<double, int>?>
	{
		protected override Dictionary<double, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDoubleIntToDictOfObjectObject : Conversion<Dictionary<double, int>?, string?>
	{
		protected override string? Converter(Dictionary<double, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleIntNullable : Conversion<string?, Dictionary<double, int?>?>
	{
		protected override Dictionary<double, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDoubleIntNullableToDictOfObjectObject : Conversion<Dictionary<double, int?>?, string?>
	{
		protected override string? Converter(Dictionary<double, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleLong : Conversion<string?, Dictionary<double, long>?>
	{
		protected override Dictionary<double, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDoubleLongToDictOfObjectObject : Conversion<Dictionary<double, long>?, string?>
	{
		protected override string? Converter(Dictionary<double, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleLongNullable : Conversion<string?, Dictionary<double, long?>?>
	{
		protected override Dictionary<double, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDoubleLongNullableToDictOfObjectObject : Conversion<Dictionary<double, long?>?, string?>
	{
		protected override string? Converter(Dictionary<double, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleFloat : Conversion<string?, Dictionary<double, float>?>
	{
		protected override Dictionary<double, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfDoubleFloatToDictOfObjectObject : Conversion<Dictionary<double, float>?, string?>
	{
		protected override string? Converter(Dictionary<double, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleFloatNullable : Conversion<string?, Dictionary<double, float?>?>
	{
		protected override Dictionary<double, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfDoubleFloatNullableToDictOfObjectObject : Conversion<Dictionary<double, float?>?, string?>
	{
		protected override string? Converter(Dictionary<double, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleDouble : Conversion<string?, Dictionary<double, double>?>
	{
		protected override Dictionary<double, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfDoubleDoubleToDictOfObjectObject : Conversion<Dictionary<double, double>?, string?>
	{
		protected override string? Converter(Dictionary<double, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleDoubleNullable : Conversion<string?, Dictionary<double, double?>?>
	{
		protected override Dictionary<double, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, double?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfDoubleDoubleNullableToDictOfObjectObject : Conversion<Dictionary<double, double?>?, string?>
	{
		protected override string? Converter(Dictionary<double, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleChar : Conversion<string?, Dictionary<double, char>?>
	{
		protected override Dictionary<double, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfDoubleCharToDictOfObjectObject : Conversion<Dictionary<double, char>?, string?>
	{
		protected override string? Converter(Dictionary<double, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleCharNullable : Conversion<string?, Dictionary<double, char?>?>
	{
		protected override Dictionary<double, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDoubleCharNullableToDictOfObjectObject : Conversion<Dictionary<double, char?>?, string?>
	{
		protected override string? Converter(Dictionary<double, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleString : Conversion<string?, Dictionary<double, string?>?>
	{
		protected override Dictionary<double, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDoubleStringToDictOfObjectObject : Conversion<Dictionary<double, string?>?, string?>
	{
		protected override string? Converter(Dictionary<double, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleDateTime : Conversion<string?, Dictionary<double, DateTime>?>
	{
		protected override Dictionary<double, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDoubleDateTimeToDictOfObjectObject : Conversion<Dictionary<double, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<double, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleDateTimeNullable : Conversion<string?, Dictionary<double, DateTime?>?>
	{
		protected override Dictionary<double, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDoubleDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<double, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<double, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleGuid : Conversion<string?, Dictionary<double, Guid>?>
	{
		protected override Dictionary<double, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfDoubleGuidToDictOfObjectObject : Conversion<Dictionary<double, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<double, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleGuidNullable : Conversion<string?, Dictionary<double, Guid?>?>
	{
		protected override Dictionary<double, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, string>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDoubleGuidNullableToDictOfObjectObject : Conversion<Dictionary<double, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<double, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleDecimal : Conversion<string?, Dictionary<double, decimal>?>
	{
		protected override Dictionary<double, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDoubleDecimalToDictOfObjectObject : Conversion<Dictionary<double, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<double, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleDecimalNullable : Conversion<string?, Dictionary<double, decimal?>?>
	{
		protected override Dictionary<double, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, long?>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDoubleDecimalNullableToDictOfObjectObject : Conversion<Dictionary<double, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<double, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDoubleCompressedString : Conversion<string?, Dictionary<double, CompressedString?>?>
	{
		protected override Dictionary<double, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<double, byte[]>>().ToDictionary(item => Conversion<double, double>.Convert((double)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfDoubleCompressedStringToDictOfObjectObject : Conversion<Dictionary<double, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<double, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<double, double>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharBool : Conversion<string?, Dictionary<char, bool>?>
	{
		protected override Dictionary<char, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, bool>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfCharBoolToDictOfObjectObject : Conversion<Dictionary<char, bool>?, string?>
	{
		protected override string? Converter(Dictionary<char, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharBoolNullable : Conversion<string?, Dictionary<char, bool?>?>
	{
		protected override Dictionary<char, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, bool?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfCharBoolNullableToDictOfObjectObject : Conversion<Dictionary<char, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<char, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharSbyte : Conversion<string?, Dictionary<char, sbyte>?>
	{
		protected override Dictionary<char, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCharSbyteToDictOfObjectObject : Conversion<Dictionary<char, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<char, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharSbyteNullable : Conversion<string?, Dictionary<char, sbyte?>?>
	{
		protected override Dictionary<char, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCharSbyteNullableToDictOfObjectObject : Conversion<Dictionary<char, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<char, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharShort : Conversion<string?, Dictionary<char, short>?>
	{
		protected override Dictionary<char, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCharShortToDictOfObjectObject : Conversion<Dictionary<char, short>?, string?>
	{
		protected override string? Converter(Dictionary<char, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharShortNullable : Conversion<string?, Dictionary<char, short?>?>
	{
		protected override Dictionary<char, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCharShortNullableToDictOfObjectObject : Conversion<Dictionary<char, short?>?, string?>
	{
		protected override string? Converter(Dictionary<char, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharInt : Conversion<string?, Dictionary<char, int>?>
	{
		protected override Dictionary<char, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCharIntToDictOfObjectObject : Conversion<Dictionary<char, int>?, string?>
	{
		protected override string? Converter(Dictionary<char, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharIntNullable : Conversion<string?, Dictionary<char, int?>?>
	{
		protected override Dictionary<char, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCharIntNullableToDictOfObjectObject : Conversion<Dictionary<char, int?>?, string?>
	{
		protected override string? Converter(Dictionary<char, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharLong : Conversion<string?, Dictionary<char, long>?>
	{
		protected override Dictionary<char, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCharLongToDictOfObjectObject : Conversion<Dictionary<char, long>?, string?>
	{
		protected override string? Converter(Dictionary<char, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharLongNullable : Conversion<string?, Dictionary<char, long?>?>
	{
		protected override Dictionary<char, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCharLongNullableToDictOfObjectObject : Conversion<Dictionary<char, long?>?, string?>
	{
		protected override string? Converter(Dictionary<char, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharFloat : Conversion<string?, Dictionary<char, float>?>
	{
		protected override Dictionary<char, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfCharFloatToDictOfObjectObject : Conversion<Dictionary<char, float>?, string?>
	{
		protected override string? Converter(Dictionary<char, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharFloatNullable : Conversion<string?, Dictionary<char, float?>?>
	{
		protected override Dictionary<char, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfCharFloatNullableToDictOfObjectObject : Conversion<Dictionary<char, float?>?, string?>
	{
		protected override string? Converter(Dictionary<char, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharDouble : Conversion<string?, Dictionary<char, double>?>
	{
		protected override Dictionary<char, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfCharDoubleToDictOfObjectObject : Conversion<Dictionary<char, double>?, string?>
	{
		protected override string? Converter(Dictionary<char, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharDoubleNullable : Conversion<string?, Dictionary<char, double?>?>
	{
		protected override Dictionary<char, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfCharDoubleNullableToDictOfObjectObject : Conversion<Dictionary<char, double?>?, string?>
	{
		protected override string? Converter(Dictionary<char, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharChar : Conversion<string?, Dictionary<char, char>?>
	{
		protected override Dictionary<char, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfCharCharToDictOfObjectObject : Conversion<Dictionary<char, char>?, string?>
	{
		protected override string? Converter(Dictionary<char, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharCharNullable : Conversion<string?, Dictionary<char, char?>?>
	{
		protected override Dictionary<char, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfCharCharNullableToDictOfObjectObject : Conversion<Dictionary<char, char?>?, string?>
	{
		protected override string? Converter(Dictionary<char, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharString : Conversion<string?, Dictionary<char, string?>?>
	{
		protected override Dictionary<char, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfCharStringToDictOfObjectObject : Conversion<Dictionary<char, string?>?, string?>
	{
		protected override string? Converter(Dictionary<char, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharDateTime : Conversion<string?, Dictionary<char, DateTime>?>
	{
		protected override Dictionary<char, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCharDateTimeToDictOfObjectObject : Conversion<Dictionary<char, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<char, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharDateTimeNullable : Conversion<string?, Dictionary<char, DateTime?>?>
	{
		protected override Dictionary<char, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCharDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<char, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<char, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharGuid : Conversion<string?, Dictionary<char, Guid>?>
	{
		protected override Dictionary<char, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfCharGuidToDictOfObjectObject : Conversion<Dictionary<char, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<char, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharGuidNullable : Conversion<string?, Dictionary<char, Guid?>?>
	{
		protected override Dictionary<char, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfCharGuidNullableToDictOfObjectObject : Conversion<Dictionary<char, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<char, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharDecimal : Conversion<string?, Dictionary<char, decimal>?>
	{
		protected override Dictionary<char, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCharDecimalToDictOfObjectObject : Conversion<Dictionary<char, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<char, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharDecimalNullable : Conversion<string?, Dictionary<char, decimal?>?>
	{
		protected override Dictionary<char, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCharDecimalNullableToDictOfObjectObject : Conversion<Dictionary<char, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<char, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCharCompressedString : Conversion<string?, Dictionary<char, CompressedString?>?>
	{
		protected override Dictionary<char, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, byte[]>>().ToDictionary(item => Conversion<string, char>.Convert((string)item.Key)!, item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfCharCompressedStringToDictOfObjectObject : Conversion<Dictionary<char, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<char, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<char, string>.Convert(item.Key)!, item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringBool : Conversion<string?, Dictionary<string, bool>?>
	{
		protected override Dictionary<string, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, bool>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfStringBoolToDictOfObjectObject : Conversion<Dictionary<string, bool>?, string?>
	{
		protected override string? Converter(Dictionary<string, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringBoolNullable : Conversion<string?, Dictionary<string, bool?>?>
	{
		protected override Dictionary<string, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, bool?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfStringBoolNullableToDictOfObjectObject : Conversion<Dictionary<string, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<string, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringSbyte : Conversion<string?, Dictionary<string, sbyte>?>
	{
		protected override Dictionary<string, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfStringSbyteToDictOfObjectObject : Conversion<Dictionary<string, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<string, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringSbyteNullable : Conversion<string?, Dictionary<string, sbyte?>?>
	{
		protected override Dictionary<string, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfStringSbyteNullableToDictOfObjectObject : Conversion<Dictionary<string, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<string, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringShort : Conversion<string?, Dictionary<string, short>?>
	{
		protected override Dictionary<string, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfStringShortToDictOfObjectObject : Conversion<Dictionary<string, short>?, string?>
	{
		protected override string? Converter(Dictionary<string, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringShortNullable : Conversion<string?, Dictionary<string, short?>?>
	{
		protected override Dictionary<string, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfStringShortNullableToDictOfObjectObject : Conversion<Dictionary<string, short?>?, string?>
	{
		protected override string? Converter(Dictionary<string, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringInt : Conversion<string?, Dictionary<string, int>?>
	{
		protected override Dictionary<string, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfStringIntToDictOfObjectObject : Conversion<Dictionary<string, int>?, string?>
	{
		protected override string? Converter(Dictionary<string, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringIntNullable : Conversion<string?, Dictionary<string, int?>?>
	{
		protected override Dictionary<string, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfStringIntNullableToDictOfObjectObject : Conversion<Dictionary<string, int?>?, string?>
	{
		protected override string? Converter(Dictionary<string, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringLong : Conversion<string?, Dictionary<string, long>?>
	{
		protected override Dictionary<string, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfStringLongToDictOfObjectObject : Conversion<Dictionary<string, long>?, string?>
	{
		protected override string? Converter(Dictionary<string, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringLongNullable : Conversion<string?, Dictionary<string, long?>?>
	{
		protected override Dictionary<string, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfStringLongNullableToDictOfObjectObject : Conversion<Dictionary<string, long?>?, string?>
	{
		protected override string? Converter(Dictionary<string, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringFloat : Conversion<string?, Dictionary<string, float>?>
	{
		protected override Dictionary<string, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfStringFloatToDictOfObjectObject : Conversion<Dictionary<string, float>?, string?>
	{
		protected override string? Converter(Dictionary<string, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringFloatNullable : Conversion<string?, Dictionary<string, float?>?>
	{
		protected override Dictionary<string, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfStringFloatNullableToDictOfObjectObject : Conversion<Dictionary<string, float?>?, string?>
	{
		protected override string? Converter(Dictionary<string, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringDouble : Conversion<string?, Dictionary<string, double>?>
	{
		protected override Dictionary<string, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfStringDoubleToDictOfObjectObject : Conversion<Dictionary<string, double>?, string?>
	{
		protected override string? Converter(Dictionary<string, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringDoubleNullable : Conversion<string?, Dictionary<string, double?>?>
	{
		protected override Dictionary<string, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfStringDoubleNullableToDictOfObjectObject : Conversion<Dictionary<string, double?>?, string?>
	{
		protected override string? Converter(Dictionary<string, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringChar : Conversion<string?, Dictionary<string, char>?>
	{
		protected override Dictionary<string, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfStringCharToDictOfObjectObject : Conversion<Dictionary<string, char>?, string?>
	{
		protected override string? Converter(Dictionary<string, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringCharNullable : Conversion<string?, Dictionary<string, char?>?>
	{
		protected override Dictionary<string, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfStringCharNullableToDictOfObjectObject : Conversion<Dictionary<string, char?>?, string?>
	{
		protected override string? Converter(Dictionary<string, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringString : Conversion<string?, Dictionary<string, string?>?>
	{
		protected override Dictionary<string, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfStringStringToDictOfObjectObject : Conversion<Dictionary<string, string?>?, string?>
	{
		protected override string? Converter(Dictionary<string, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringDateTime : Conversion<string?, Dictionary<string, DateTime>?>
	{
		protected override Dictionary<string, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfStringDateTimeToDictOfObjectObject : Conversion<Dictionary<string, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<string, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringDateTimeNullable : Conversion<string?, Dictionary<string, DateTime?>?>
	{
		protected override Dictionary<string, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfStringDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<string, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<string, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringGuid : Conversion<string?, Dictionary<string, Guid>?>
	{
		protected override Dictionary<string, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfStringGuidToDictOfObjectObject : Conversion<Dictionary<string, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<string, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringGuidNullable : Conversion<string?, Dictionary<string, Guid?>?>
	{
		protected override Dictionary<string, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfStringGuidNullableToDictOfObjectObject : Conversion<Dictionary<string, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<string, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringDecimal : Conversion<string?, Dictionary<string, decimal>?>
	{
		protected override Dictionary<string, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfStringDecimalToDictOfObjectObject : Conversion<Dictionary<string, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<string, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringDecimalNullable : Conversion<string?, Dictionary<string, decimal?>?>
	{
		protected override Dictionary<string, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfStringDecimalNullableToDictOfObjectObject : Conversion<Dictionary<string, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<string, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfStringCompressedString : Conversion<string?, Dictionary<string, CompressedString?>?>
	{
		protected override Dictionary<string, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, byte[]>>().ToDictionary(item => Conversion<string, string>.Convert((string)item.Key)!, item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfStringCompressedStringToDictOfObjectObject : Conversion<Dictionary<string, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<string, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<string, string>.Convert(item.Key)!, item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeBool : Conversion<string?, Dictionary<DateTime, bool>?>
	{
		protected override Dictionary<DateTime, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfDateTimeBoolToDictOfObjectObject : Conversion<Dictionary<DateTime, bool>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeBoolNullable : Conversion<string?, Dictionary<DateTime, bool?>?>
	{
		protected override Dictionary<DateTime, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeBoolNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeSbyte : Conversion<string?, Dictionary<DateTime, sbyte>?>
	{
		protected override Dictionary<DateTime, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDateTimeSbyteToDictOfObjectObject : Conversion<Dictionary<DateTime, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeSbyteNullable : Conversion<string?, Dictionary<DateTime, sbyte?>?>
	{
		protected override Dictionary<DateTime, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeSbyteNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeShort : Conversion<string?, Dictionary<DateTime, short>?>
	{
		protected override Dictionary<DateTime, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDateTimeShortToDictOfObjectObject : Conversion<Dictionary<DateTime, short>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeShortNullable : Conversion<string?, Dictionary<DateTime, short?>?>
	{
		protected override Dictionary<DateTime, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeShortNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, short?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeInt : Conversion<string?, Dictionary<DateTime, int>?>
	{
		protected override Dictionary<DateTime, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDateTimeIntToDictOfObjectObject : Conversion<Dictionary<DateTime, int>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeIntNullable : Conversion<string?, Dictionary<DateTime, int?>?>
	{
		protected override Dictionary<DateTime, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeIntNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, int?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeLong : Conversion<string?, Dictionary<DateTime, long>?>
	{
		protected override Dictionary<DateTime, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDateTimeLongToDictOfObjectObject : Conversion<Dictionary<DateTime, long>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeLongNullable : Conversion<string?, Dictionary<DateTime, long?>?>
	{
		protected override Dictionary<DateTime, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeLongNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, long?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeFloat : Conversion<string?, Dictionary<DateTime, float>?>
	{
		protected override Dictionary<DateTime, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfDateTimeFloatToDictOfObjectObject : Conversion<Dictionary<DateTime, float>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeFloatNullable : Conversion<string?, Dictionary<DateTime, float?>?>
	{
		protected override Dictionary<DateTime, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeFloatNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, float?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeDouble : Conversion<string?, Dictionary<DateTime, double>?>
	{
		protected override Dictionary<DateTime, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfDateTimeDoubleToDictOfObjectObject : Conversion<Dictionary<DateTime, double>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeDoubleNullable : Conversion<string?, Dictionary<DateTime, double?>?>
	{
		protected override Dictionary<DateTime, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeDoubleNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, double?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeChar : Conversion<string?, Dictionary<DateTime, char>?>
	{
		protected override Dictionary<DateTime, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfDateTimeCharToDictOfObjectObject : Conversion<Dictionary<DateTime, char>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeCharNullable : Conversion<string?, Dictionary<DateTime, char?>?>
	{
		protected override Dictionary<DateTime, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDateTimeCharNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, char?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeString : Conversion<string?, Dictionary<DateTime, string?>?>
	{
		protected override Dictionary<DateTime, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDateTimeStringToDictOfObjectObject : Conversion<Dictionary<DateTime, string?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeDateTime : Conversion<string?, Dictionary<DateTime, DateTime>?>
	{
		protected override Dictionary<DateTime, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDateTimeDateTimeToDictOfObjectObject : Conversion<Dictionary<DateTime, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeDateTimeNullable : Conversion<string?, Dictionary<DateTime, DateTime?>?>
	{
		protected override Dictionary<DateTime, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeGuid : Conversion<string?, Dictionary<DateTime, Guid>?>
	{
		protected override Dictionary<DateTime, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfDateTimeGuidToDictOfObjectObject : Conversion<Dictionary<DateTime, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeGuidNullable : Conversion<string?, Dictionary<DateTime, Guid?>?>
	{
		protected override Dictionary<DateTime, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDateTimeGuidNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeDecimal : Conversion<string?, Dictionary<DateTime, decimal>?>
	{
		protected override Dictionary<DateTime, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDateTimeDecimalToDictOfObjectObject : Conversion<Dictionary<DateTime, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeDecimalNullable : Conversion<string?, Dictionary<DateTime, decimal?>?>
	{
		protected override Dictionary<DateTime, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDateTimeDecimalNullableToDictOfObjectObject : Conversion<Dictionary<DateTime, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDateTimeCompressedString : Conversion<string?, Dictionary<DateTime, CompressedString?>?>
	{
		protected override Dictionary<DateTime, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, byte[]>>().ToDictionary(item => Conversion<long, DateTime>.Convert((long)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfDateTimeCompressedStringToDictOfObjectObject : Conversion<Dictionary<DateTime, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<DateTime, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<DateTime, long>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidBool : Conversion<string?, Dictionary<Guid, bool>?>
	{
		protected override Dictionary<Guid, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, bool>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfGuidBoolToDictOfObjectObject : Conversion<Dictionary<Guid, bool>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidBoolNullable : Conversion<string?, Dictionary<Guid, bool?>?>
	{
		protected override Dictionary<Guid, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, bool?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfGuidBoolNullableToDictOfObjectObject : Conversion<Dictionary<Guid, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidSbyte : Conversion<string?, Dictionary<Guid, sbyte>?>
	{
		protected override Dictionary<Guid, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfGuidSbyteToDictOfObjectObject : Conversion<Dictionary<Guid, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidSbyteNullable : Conversion<string?, Dictionary<Guid, sbyte?>?>
	{
		protected override Dictionary<Guid, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfGuidSbyteNullableToDictOfObjectObject : Conversion<Dictionary<Guid, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidShort : Conversion<string?, Dictionary<Guid, short>?>
	{
		protected override Dictionary<Guid, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfGuidShortToDictOfObjectObject : Conversion<Dictionary<Guid, short>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidShortNullable : Conversion<string?, Dictionary<Guid, short?>?>
	{
		protected override Dictionary<Guid, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfGuidShortNullableToDictOfObjectObject : Conversion<Dictionary<Guid, short?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidInt : Conversion<string?, Dictionary<Guid, int>?>
	{
		protected override Dictionary<Guid, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfGuidIntToDictOfObjectObject : Conversion<Dictionary<Guid, int>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidIntNullable : Conversion<string?, Dictionary<Guid, int?>?>
	{
		protected override Dictionary<Guid, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfGuidIntNullableToDictOfObjectObject : Conversion<Dictionary<Guid, int?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidLong : Conversion<string?, Dictionary<Guid, long>?>
	{
		protected override Dictionary<Guid, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfGuidLongToDictOfObjectObject : Conversion<Dictionary<Guid, long>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidLongNullable : Conversion<string?, Dictionary<Guid, long?>?>
	{
		protected override Dictionary<Guid, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfGuidLongNullableToDictOfObjectObject : Conversion<Dictionary<Guid, long?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidFloat : Conversion<string?, Dictionary<Guid, float>?>
	{
		protected override Dictionary<Guid, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfGuidFloatToDictOfObjectObject : Conversion<Dictionary<Guid, float>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidFloatNullable : Conversion<string?, Dictionary<Guid, float?>?>
	{
		protected override Dictionary<Guid, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfGuidFloatNullableToDictOfObjectObject : Conversion<Dictionary<Guid, float?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidDouble : Conversion<string?, Dictionary<Guid, double>?>
	{
		protected override Dictionary<Guid, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfGuidDoubleToDictOfObjectObject : Conversion<Dictionary<Guid, double>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidDoubleNullable : Conversion<string?, Dictionary<Guid, double?>?>
	{
		protected override Dictionary<Guid, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, double?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfGuidDoubleNullableToDictOfObjectObject : Conversion<Dictionary<Guid, double?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidChar : Conversion<string?, Dictionary<Guid, char>?>
	{
		protected override Dictionary<Guid, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfGuidCharToDictOfObjectObject : Conversion<Dictionary<Guid, char>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidCharNullable : Conversion<string?, Dictionary<Guid, char?>?>
	{
		protected override Dictionary<Guid, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfGuidCharNullableToDictOfObjectObject : Conversion<Dictionary<Guid, char?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidString : Conversion<string?, Dictionary<Guid, string?>?>
	{
		protected override Dictionary<Guid, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfGuidStringToDictOfObjectObject : Conversion<Dictionary<Guid, string?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidDateTime : Conversion<string?, Dictionary<Guid, DateTime>?>
	{
		protected override Dictionary<Guid, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfGuidDateTimeToDictOfObjectObject : Conversion<Dictionary<Guid, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidDateTimeNullable : Conversion<string?, Dictionary<Guid, DateTime?>?>
	{
		protected override Dictionary<Guid, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfGuidDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<Guid, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidGuid : Conversion<string?, Dictionary<Guid, Guid>?>
	{
		protected override Dictionary<Guid, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfGuidGuidToDictOfObjectObject : Conversion<Dictionary<Guid, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidGuidNullable : Conversion<string?, Dictionary<Guid, Guid?>?>
	{
		protected override Dictionary<Guid, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, string>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfGuidGuidNullableToDictOfObjectObject : Conversion<Dictionary<Guid, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidDecimal : Conversion<string?, Dictionary<Guid, decimal>?>
	{
		protected override Dictionary<Guid, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfGuidDecimalToDictOfObjectObject : Conversion<Dictionary<Guid, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidDecimalNullable : Conversion<string?, Dictionary<Guid, decimal?>?>
	{
		protected override Dictionary<Guid, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, long?>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfGuidDecimalNullableToDictOfObjectObject : Conversion<Dictionary<Guid, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfGuidCompressedString : Conversion<string?, Dictionary<Guid, CompressedString?>?>
	{
		protected override Dictionary<Guid, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<string, byte[]>>().ToDictionary(item => Conversion<string, Guid>.Convert((string)item.Key)!, item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfGuidCompressedStringToDictOfObjectObject : Conversion<Dictionary<Guid, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<Guid, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<Guid, string>.Convert(item.Key)!, item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalBool : Conversion<string?, Dictionary<decimal, bool>?>
	{
		protected override Dictionary<decimal, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfDecimalBoolToDictOfObjectObject : Conversion<Dictionary<decimal, bool>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalBoolNullable : Conversion<string?, Dictionary<decimal, bool?>?>
	{
		protected override Dictionary<decimal, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, bool?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfDecimalBoolNullableToDictOfObjectObject : Conversion<Dictionary<decimal, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalSbyte : Conversion<string?, Dictionary<decimal, sbyte>?>
	{
		protected override Dictionary<decimal, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDecimalSbyteToDictOfObjectObject : Conversion<Dictionary<decimal, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalSbyteNullable : Conversion<string?, Dictionary<decimal, sbyte?>?>
	{
		protected override Dictionary<decimal, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDecimalSbyteNullableToDictOfObjectObject : Conversion<Dictionary<decimal, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalShort : Conversion<string?, Dictionary<decimal, short>?>
	{
		protected override Dictionary<decimal, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDecimalShortToDictOfObjectObject : Conversion<Dictionary<decimal, short>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalShortNullable : Conversion<string?, Dictionary<decimal, short?>?>
	{
		protected override Dictionary<decimal, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDecimalShortNullableToDictOfObjectObject : Conversion<Dictionary<decimal, short?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalInt : Conversion<string?, Dictionary<decimal, int>?>
	{
		protected override Dictionary<decimal, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDecimalIntToDictOfObjectObject : Conversion<Dictionary<decimal, int>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalIntNullable : Conversion<string?, Dictionary<decimal, int?>?>
	{
		protected override Dictionary<decimal, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDecimalIntNullableToDictOfObjectObject : Conversion<Dictionary<decimal, int?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalLong : Conversion<string?, Dictionary<decimal, long>?>
	{
		protected override Dictionary<decimal, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDecimalLongToDictOfObjectObject : Conversion<Dictionary<decimal, long>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalLongNullable : Conversion<string?, Dictionary<decimal, long?>?>
	{
		protected override Dictionary<decimal, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDecimalLongNullableToDictOfObjectObject : Conversion<Dictionary<decimal, long?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalFloat : Conversion<string?, Dictionary<decimal, float>?>
	{
		protected override Dictionary<decimal, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfDecimalFloatToDictOfObjectObject : Conversion<Dictionary<decimal, float>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalFloatNullable : Conversion<string?, Dictionary<decimal, float?>?>
	{
		protected override Dictionary<decimal, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfDecimalFloatNullableToDictOfObjectObject : Conversion<Dictionary<decimal, float?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalDouble : Conversion<string?, Dictionary<decimal, double>?>
	{
		protected override Dictionary<decimal, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfDecimalDoubleToDictOfObjectObject : Conversion<Dictionary<decimal, double>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalDoubleNullable : Conversion<string?, Dictionary<decimal, double?>?>
	{
		protected override Dictionary<decimal, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, double?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfDecimalDoubleNullableToDictOfObjectObject : Conversion<Dictionary<decimal, double?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalChar : Conversion<string?, Dictionary<decimal, char>?>
	{
		protected override Dictionary<decimal, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfDecimalCharToDictOfObjectObject : Conversion<Dictionary<decimal, char>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalCharNullable : Conversion<string?, Dictionary<decimal, char?>?>
	{
		protected override Dictionary<decimal, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDecimalCharNullableToDictOfObjectObject : Conversion<Dictionary<decimal, char?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalString : Conversion<string?, Dictionary<decimal, string?>?>
	{
		protected override Dictionary<decimal, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDecimalStringToDictOfObjectObject : Conversion<Dictionary<decimal, string?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalDateTime : Conversion<string?, Dictionary<decimal, DateTime>?>
	{
		protected override Dictionary<decimal, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDecimalDateTimeToDictOfObjectObject : Conversion<Dictionary<decimal, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalDateTimeNullable : Conversion<string?, Dictionary<decimal, DateTime?>?>
	{
		protected override Dictionary<decimal, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDecimalDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<decimal, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalGuid : Conversion<string?, Dictionary<decimal, Guid>?>
	{
		protected override Dictionary<decimal, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfDecimalGuidToDictOfObjectObject : Conversion<Dictionary<decimal, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalGuidNullable : Conversion<string?, Dictionary<decimal, Guid?>?>
	{
		protected override Dictionary<decimal, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, string>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfDecimalGuidNullableToDictOfObjectObject : Conversion<Dictionary<decimal, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalDecimal : Conversion<string?, Dictionary<decimal, decimal>?>
	{
		protected override Dictionary<decimal, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfDecimalDecimalToDictOfObjectObject : Conversion<Dictionary<decimal, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalDecimalNullable : Conversion<string?, Dictionary<decimal, decimal?>?>
	{
		protected override Dictionary<decimal, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, long?>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfDecimalDecimalNullableToDictOfObjectObject : Conversion<Dictionary<decimal, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfDecimalCompressedString : Conversion<string?, Dictionary<decimal, CompressedString?>?>
	{
		protected override Dictionary<decimal, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<long, byte[]>>().ToDictionary(item => Conversion<long, decimal>.Convert((long)item.Key), item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfDecimalCompressedStringToDictOfObjectObject : Conversion<Dictionary<decimal, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<decimal, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<decimal, long>.Convert(item.Key), item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringBool : Conversion<string?, Dictionary<CompressedString, bool>?>
	{
		protected override Dictionary<CompressedString, bool>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], bool>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<bool, bool>.Convert((bool)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringBoolToDictOfObjectObject : Conversion<Dictionary<CompressedString, bool>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, bool>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<bool, bool>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringBoolNullable : Conversion<string?, Dictionary<CompressedString, bool?>?>
	{
		protected override Dictionary<CompressedString, bool?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], bool?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<bool?, bool?>.Convert((bool?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringBoolNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, bool?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, bool?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<bool?, bool?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringSbyte : Conversion<string?, Dictionary<CompressedString, sbyte>?>
	{
		protected override Dictionary<CompressedString, sbyte>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long, sbyte>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringSbyteToDictOfObjectObject : Conversion<Dictionary<CompressedString, sbyte>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, sbyte>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<sbyte, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringSbyteNullable : Conversion<string?, Dictionary<CompressedString, sbyte?>?>
	{
		protected override Dictionary<CompressedString, sbyte?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long?, sbyte?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringSbyteNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, sbyte?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, sbyte?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<sbyte?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringShort : Conversion<string?, Dictionary<CompressedString, short>?>
	{
		protected override Dictionary<CompressedString, short>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long, short>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringShortToDictOfObjectObject : Conversion<Dictionary<CompressedString, short>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, short>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<short, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringShortNullable : Conversion<string?, Dictionary<CompressedString, short?>?>
	{
		protected override Dictionary<CompressedString, short?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long?, short?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringShortNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, short?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, short?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<short?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringInt : Conversion<string?, Dictionary<CompressedString, int>?>
	{
		protected override Dictionary<CompressedString, int>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long, int>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringIntToDictOfObjectObject : Conversion<Dictionary<CompressedString, int>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, int>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<int, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringIntNullable : Conversion<string?, Dictionary<CompressedString, int?>?>
	{
		protected override Dictionary<CompressedString, int?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long?, int?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringIntNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, int?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, int?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<int?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringLong : Conversion<string?, Dictionary<CompressedString, long>?>
	{
		protected override Dictionary<CompressedString, long>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long, long>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringLongToDictOfObjectObject : Conversion<Dictionary<CompressedString, long>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, long>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<long, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringLongNullable : Conversion<string?, Dictionary<CompressedString, long?>?>
	{
		protected override Dictionary<CompressedString, long?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long?, long?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringLongNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, long?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, long?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<long?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringFloat : Conversion<string?, Dictionary<CompressedString, float>?>
	{
		protected override Dictionary<CompressedString, float>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], double>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<double, float>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringFloatToDictOfObjectObject : Conversion<Dictionary<CompressedString, float>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, float>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<float, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringFloatNullable : Conversion<string?, Dictionary<CompressedString, float?>?>
	{
		protected override Dictionary<CompressedString, float?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], double?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<double?, float?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringFloatNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, float?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, float?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<float?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringDouble : Conversion<string?, Dictionary<CompressedString, double>?>
	{
		protected override Dictionary<CompressedString, double>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], double>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<double, double>.Convert((double)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringDoubleToDictOfObjectObject : Conversion<Dictionary<CompressedString, double>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, double>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<double, double>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringDoubleNullable : Conversion<string?, Dictionary<CompressedString, double?>?>
	{
		protected override Dictionary<CompressedString, double?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], double?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<double?, double?>.Convert((double?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringDoubleNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, double?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, double?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<double?, double?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringChar : Conversion<string?, Dictionary<CompressedString, char>?>
	{
		protected override Dictionary<CompressedString, char>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], string>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<string, char>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringCharToDictOfObjectObject : Conversion<Dictionary<CompressedString, char>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, char>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<char, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringCharNullable : Conversion<string?, Dictionary<CompressedString, char?>?>
	{
		protected override Dictionary<CompressedString, char?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], string>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<string, char?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfCompressedStringCharNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, char?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, char?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<char?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringString : Conversion<string?, Dictionary<CompressedString, string?>?>
	{
		protected override Dictionary<CompressedString, string?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], string>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<string, string?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfCompressedStringStringToDictOfObjectObject : Conversion<Dictionary<CompressedString, string?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, string?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<string, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringDateTime : Conversion<string?, Dictionary<CompressedString, DateTime>?>
	{
		protected override Dictionary<CompressedString, DateTime>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long, DateTime>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringDateTimeToDictOfObjectObject : Conversion<Dictionary<CompressedString, DateTime>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, DateTime>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<DateTime, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringDateTimeNullable : Conversion<string?, Dictionary<CompressedString, DateTime?>?>
	{
		protected override Dictionary<CompressedString, DateTime?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long?, DateTime?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringDateTimeNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, DateTime?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, DateTime?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<DateTime?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringGuid : Conversion<string?, Dictionary<CompressedString, Guid>?>
	{
		protected override Dictionary<CompressedString, Guid>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], string>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<string, Guid>.Convert((string)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringGuidToDictOfObjectObject : Conversion<Dictionary<CompressedString, Guid>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, Guid>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<Guid, string>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringGuidNullable : Conversion<string?, Dictionary<CompressedString, Guid?>?>
	{
		protected override Dictionary<CompressedString, Guid?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], string>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<string, Guid?>.Convert((string)item.Value));
		}
	}	
	internal class DictOfCompressedStringGuidNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, Guid?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, Guid?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<Guid?, string>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringDecimal : Conversion<string?, Dictionary<CompressedString, decimal>?>
	{
		protected override Dictionary<CompressedString, decimal>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long, decimal>.Convert((long)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringDecimalToDictOfObjectObject : Conversion<Dictionary<CompressedString, decimal>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, decimal>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object)Conversion<decimal, long>.Convert(item.Value)!).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringDecimalNullable : Conversion<string?, Dictionary<CompressedString, decimal?>?>
	{
		protected override Dictionary<CompressedString, decimal?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], long?>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<long?, decimal?>.Convert((long?)item.Value)!);
		}
	}	
	internal class DictOfCompressedStringDecimalNullableToDictOfObjectObject : Conversion<Dictionary<CompressedString, decimal?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, decimal?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<decimal?, long?>.Convert(item.Value)).ToJson();
		}
	}	
	internal class DictOfObjectObjectToDictOfCompressedStringCompressedString : Conversion<string?, Dictionary<CompressedString, CompressedString?>?>
	{
		protected override Dictionary<CompressedString, CompressedString?>? Converter(string? value)
		{
			if (value is null)
				return null;

			return value.FromJson<Dictionary<byte[], byte[]>>().ToDictionary(item => Conversion<byte[], CompressedString>.Convert((byte[])item.Key)!, item => Conversion<byte[], CompressedString?>.Convert((byte[])item.Value));
		}
	}	
	internal class DictOfCompressedStringCompressedStringToDictOfObjectObject : Conversion<Dictionary<CompressedString, CompressedString?>?, string?>
	{
		protected override string? Converter(Dictionary<CompressedString, CompressedString?>? value)
		{
			if (value is null)
				return null;

			return value.ToDictionary(item => (object)Conversion<CompressedString, byte[]>.Convert(item.Key)!, item => (object?)Conversion<CompressedString, byte[]>.Convert(item.Value)).ToJson();
		}
	}	
   

	#endregion
}
