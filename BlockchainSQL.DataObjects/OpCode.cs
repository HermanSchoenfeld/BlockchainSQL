using System.ComponentModel;
// ReSharper disable InconsistentNaming

namespace BlockchainSQL.DataObjects
{
    public enum OpCode : byte {

        [Description("An empty array of bytes is pushed onto the stack. (This is not a no-op: an item is added to the stack.)")]
        OP_0 = 0x00,

        [Description("The next byte is data to be pushed onto the stack")]
        OP_PUSHBYTES01 = 0x01,

        [Description("The next 2 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES02 = 0x02,

        [Description("The next 3 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES03 = 0x03,

        [Description("The next 4 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES04 = 0x04,

        [Description("The next 5 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES05 = 0x05,

        [Description("The next 6 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES06 = 0x06,

        [Description("The next 7 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES07 = 0x07,

        [Description("The next 8 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES08 = 0x08,

        [Description("The next 9 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES09 = 0x09,

        [Description("The next 10 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES10 = 0x0A,

        [Description("The next 11 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES11 = 0x0B,

        [Description("The next 12 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES12 = 0x0C,

        [Description("The next 13 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES13 = 0x0D,

        [Description("The next 14 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES14 = 0x0E,

        [Description("The next 15 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES15 = 0x0F,

        [Description("The next 16 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES16 = 0x10,

        [Description("The next 17 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES17 = 0x11,

        [Description("The next 18 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES18 = 0x12,

        [Description("The next 19 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES19 = 0x13,

        [Description("The next 20 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES20 = 0x14,

        [Description("The next 21 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES21 = 0x15,

        [Description("The next 22 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES22 = 0x16,

        [Description("The next 23 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES23 = 0x17,

        [Description("The next 24 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES24 = 0x18,

        [Description("The next 25 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES25 = 0x19,

        [Description("The next 26 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES26 = 0x1A,

        [Description("The next 27 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES27 = 0x1B,

        [Description("The next 28 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES28 = 0x1C,

        [Description("The next 29 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES29 = 0x1D,

        [Description("The next 30 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES30 = 0x1E,

        [Description("The next 31 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES31 = 0x1F,

        [Description("The next 32 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES32 = 0x20,

        [Description("The next 33 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES33 = 0x21,

        [Description("The next 34 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES34 = 0x22,

        [Description("The next 35 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES35 = 0x23,

        [Description("The next 36 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES36 = 0x24,

        [Description("The next 37 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES37 = 0x25,

        [Description("The next 38 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES38 = 0x26,

        [Description("The next 39 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES39 = 0x27,

        [Description("The next 40 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES40 = 0x28,

        [Description("The next 41 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES41 = 0x29,

        [Description("The next 42 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES42 = 0x2A,

        [Description("The next 43 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES43 = 0x2B,

        [Description("The next 44 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES44 = 0x2C,

        [Description("The next 45 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES45 = 0x2D,

        [Description("The next 46 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES46 = 0x2E,

        [Description("The next 47 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES47 = 0x2F,

        [Description("The next 48 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES48 = 0x30,

        [Description("The next 49 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES49 = 0x31,

        [Description("The next 50 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES50 = 0x32,

        [Description("The next 51 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES51 = 0x33,

        [Description("The next 52 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES52 = 0x34,

        [Description("The next 53 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES53 = 0x35,

        [Description("The next 54 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES54 = 0x36,

        [Description("The next 55 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES55 = 0x37,

        [Description("The next 56 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES56 = 0x38,

        [Description("The next 57 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES57 = 0x39,

        [Description("The next 58 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES58 = 0x3A,

        [Description("The next 59 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES59 = 0x3B,

        [Description("The next 60 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES60 = 0x3C,

        [Description("The next 61 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES61 = 0x3D,

        [Description("The next 62 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES62 = 0x3E,

        [Description("The next 63 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES63 = 0x3F,

        [Description("The next 64 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES64 = 0x40,

        [Description("The next 65 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES65 = 0x41,

        [Description("The next 66 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES66 = 0x42,

        [Description("The next 67 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES67 = 0x43,

        [Description("The next 68 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES68 = 0x44,

        [Description("The next 69 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES69 = 0x45,

        [Description("The next 70 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES70 = 0x46,

        [Description("The next 71 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES71 = 0x47,

        [Description("The next 72 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES72 = 0x48,

        [Description("The next 73 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES73 = 0x49,

        [Description("The next 74 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES74 = 0x4A,

        [Description("The next 75 bytes is data to be pushed onto the stack")]
        OP_PUSHBYTES75 = 0x4B,

        [Description("The next byte contains the number of bytes to be pushed onto the stack.")]
        OP_PUSHDATA1 = 0x4C,

        [Description("The next two bytes contain the number of bytes to be pushed onto the stack.")]
        OP_PUSHDATA2 = 0x4D,

        [Description("The next four bytes contain the number of bytes to be pushed onto the stack.")]
        OP_PUSHDATA4 = 0x4E,

        [Description("The number -1 is pushed onto the stack.")]
        OP_1NEGATE = 0x4F,

        [Description("The number 1 is pushed onto the stack.")]
        OP_1 = 0x51,

        [Description("The number 2 is pushed onto the stack.")]
        OP_2 = 0x52,

        [Description("The number 3 is pushed onto the stack.")]
        OP_3 = 0x53,

        [Description("The number 4 is pushed onto the stack.")]
        OP_4 = 0x54,

        [Description("The number 5 is pushed onto the stack.")]
        OP_5 = 0x55,

        [Description("The number 6 is pushed onto the stack.")]
        OP_6 = 0x56,

        [Description("The number 7 is pushed onto the stack.")]
        OP_7 = 0x57,

        [Description("The number 8 is pushed onto the stack.")]
        OP_8 = 0x58,

        [Description("The number 9 is pushed onto the stack.")]
        OP_9 = 0x59,

        [Description("The number 10 is pushed onto the stack.")]
        OP_10 = 0x5A,

        [Description("The number 11 is pushed onto the stack.")]
        OP_11 = 0x5B,

        [Description("The number 12 is pushed onto the stack.")]
        OP_12 = 0x5C,

        [Description("The number 13 is pushed onto the stack.")]
        OP_13 = 0x5D,

        [Description("The number 14 is pushed onto the stack.")]
        OP_14 = 0x5E,

        [Description("The number 15 is pushed onto the stack.")]
        OP_15 = 0x5F,

        [Description("The number 16 is pushed onto the stack.")]
        OP_16 = 0x60,

        // Flow control
        [Description("Does nothing.")]
        OP_NOP = 0x61,

        [Description("If the top stack value is not 0, the statements are executed. The top stack value is removed.")]
        OP_IF = 0x63,

        [Description("If the top stack value is 0, the statements are executed. The top stack value is removed.")]
        OP_NOTIF = 0x64,

        [Description("If the preceding OP_IF or OP_NOTIF or OP_ELSE was not executed then these statements are and if the preceding OP_IF or OP_NOTIF or OP_ELSE was executed then these statements are not.")]
        OP_ELSE = 0x67,

        [Description("Ends an if/else block.")]
        OP_ENDIF = 0x68,

        [Description("Marks transaction as invalid if top stack value is not true. True is removed, but false is not.")]
        OP_VERIFY = 0x69,

        [Description("Marks transaction as invalid.")]
        OP_RETURN = 0x6A,

        // Stack
        [Description("Puts the input onto the top of the alt stack. Removes it from the main stack.")]
        OP_TOALTSTACK = 0x6B,

        [Description("Puts the input onto the top of the main stack. Removes it from the alt stack.")]
        OP_FROMALTSTACK = 0x6C,

        [Description("If the top stack value is not 0, duplicate it.")]
        OP_IFDUP = 0x73,

        [Description("Puts the number of stack items onto the stack.")]
        OP_DEPTH = 0x74,

        [Description("Removes the top stack item.")]
        OP_DROP = 0x75,

        [Description("Duplicates the top stack item.")]
        OP_DUP = 0x76,

        [Description("Removes the second-to-top stack item.")]
        OP_NIP = 0x77,

        [Description("Copies the second-to-top stack item to the top.")]
        OP_OVER = 0x78,

        [Description("The item n back in the stack is copied to the top.")]
        OP_PICK = 0x79,

        [Description("The item n back in the stack is moved to the top.")]
        OP_ROLL = 0x7A,

        [Description("The top three items on the stack are rotated to the left.")]
        OP_ROT = 0x7B,

        [Description("The top two items on the stack are swapped.")]
        OP_SWAP = 0x7C,

        [Description("The item at the top of the stack is copied and inserted before the second-to-top item.")]
        OP_TUCK = 0x7D,

        [Description("Removes the top two stack items.")]
        OP_2DROP = 0x6D,

        [Description("Duplicates the top two stack items.")]
        OP_2DUP = 0x6E,

        [Description("Duplicates the top three stack items.")]
        OP_3DUP = 0x6F,

        [Description("Copies the pair of items two spaces back in the stack to the front.")]
        OP_2OVER = 0x70,

        [Description("The fifth and sixth items back are moved to the top of the stack.")]
        OP_2ROT = 0x71,

        [Description("Swaps the top two pairs of items.")]
        OP_2SWAP = 0x72,

        // Splice
        [Description("Concatenates two strings. Currently disabled.")]
        OP_CAT = 0x7E,

        [Description("Returns a section of a string. Currently disabled.")]
        OP_SUBSTR = 0x7F,

        [Description("Keeps only characters left of the specified point in a string. Currently disabled.")]
        OP_LEFT = 0x80,

        [Description("Keeps only characters right of the specified point in a string. Currently disabled.")]
        OP_RIGHT = 0x81,

        [Description("Returns the length of the input string.")]
        OP_SIZE = 0x82,

        // Bitwise logic
        [Description("Flips all of the bits in the input. Currently disabled.")]
        OP_INVERT = 0x83,

        [Description("Boolean and between each bit in the inputs. Currently disabled.")]
        OP_AND = 0x84,

        [Description("Boolean or between each bit in the inputs. Currently disabled.")]
        OP_OR = 0x85,

        [Description("Boolean exclusive or between each bit in the inputs. Currently disabled.")]
        OP_XOR = 0x86,

        [Description("Returns 1 if the inputs are exactly equal, 0 otherwise.")]
        OP_EQUAL = 0x87,

        [Description("Same as OP_EQUAL, but runs OP_VERIFY afterward.")]
        OP_EQUALVERIFY = 0x88,

        // Arithmetic
        // Note: Arithmetic inputs are limited to signed 32-bit integers, but may overflow their output.
        [Description("1 is added to the input.")]
        OP_1ADD = 0x8B,

        [Description("1 is subtracted from the input.")]
        OP_1SUB = 0x8C,

        [Description("The input is multiplied by 2. Currently disabled.")]
        OP_2MUL = 0x8D,

        [Description("The input is divided by 2. Currently disabled.")]
        OP_2DIV = 0x8E,

        [Description("The sign of the input is flipped.")]
        OP_NEGATE = 0x8F,

        [Description("The input is made positive.")]
        OP_ABS = 0x90,

        [Description("If the input is 0 or 1, it is flipped. Otherwise the output will be 0.")]
        OP_NOT = 0x91,

        [Description("Returns 0 if the input is 0. 1 otherwise.")]
        OP_0NOTEQUAL = 0x92,

        [Description("a is added to b.")]
        OP_ADD = 0x93,

        [Description("b is subtracted from a.")]
        OP_SUB = 0x94,

        [Description("a is multiplied by b. Currently disabled.")]
        OP_MUL = 0x95,

        [Description("a is divided by b. Currently disabled.")]
        OP_DIV = 0x96,

        [Description("Returns the remainder after dividing a by b. Currently disabled.")]
        OP_MOD = 0x97,

        [Description("Shifts a left b bits, preserving sign. Currently disabled.")]
        OP_LSHIFT = 0x98,

        [Description("Shifts a right b bits, preserving sign. Currently disabled.")]
        OP_RSHIFT = 0x99,

        [Description("If both a and b are not 0, the output is 1. Otherwise 0.")]
        OP_BOOLAND = 0x9A,

        [Description("If a or b is not 0, the output is 1. Otherwise 0.")]
        OP_BOOLOR = 0x9B,

        [Description("Returns 1 if the numbers are equal, 0 otherwise.")]
        OP_NUMEQUAL = 0x9C,

        [Description("Same as OP_NUMEQUAL, but runs OP_VERIFY afterward.")]
        OP_NUMEQUALVERIFY = 0x9D,

        [Description("Returns 1 if the numbers are not equal, 0 otherwise.")]
        OP_NUMNOTEQUAL = 0x9E,

        [Description("Returns 1 if a is less than b, 0 otherwise.")]
        OP_LESSTHAN = 0x9F,

        [Description("Returns 1 if a is greater than b, 0 otherwise.")]
        OP_GREATERTHAN = 0xA0,

        [Description("Returns 1 if a is less than or equal to b, 0 otherwise.")]
        OP_LESSTHANOREQUAL = 0xA1,

        [Description("Returns 1 if a is greater than or equal to b, 0 otherwise.")]
        OP_GREATERTHANOREQUAL = 0xA2,

        [Description("Returns the smaller of a and b.")]
        OP_MIN = 0xA3,

        [Description("Returns the larger of a and b.")]
        OP_MAX = 0xA4,

        [Description("Returns 1 if x is within the specified range (left-inclusive), 0 otherwise.")]
        OP_WITHIN = 0xA5,

        // Crypto
        [Description("The input is hashed using RIPEMD-160.")]
        OP_RIPEMD160 = 0xA6,

        [Description("The input is hashed using SHA-1.")]
        OP_SHA1 = 0xA7,

        [Description("The input is hashed using SHA-256.")]
        OP_SHA256 = 0xA8,

        [Description("The input is hashed twice: first with SHA-256 and then with RIPEMD-160.")]
        OP_HASH160 = 0xA9,

        [Description("The input is hashed two times with SHA-256.")]
        OP_HASH256 = 0xAA,

        [Description("All of the signature checking words will only match signatures to the data after the most recently-executed OP_CODESEPARATOR.")]
        OP_CODESEPARATOR = 0xAB,

        [Description("The entire transaction's outputs, inputs, and script (from the most recently-executed OP_CODESEPARATOR to the end) are hashed. The signature used by OP_CHECKSIG must be a valid signature for this hash and public key. If it is, 1 is returned, 0 otherwise.")]
        OP_CHECKSIG = 0xAC,

        [Description("Same as OP_CHECKSIG, but OP_VERIFY is executed afterward.")]
        OP_CHECKSIGVERIFY = 0xAD,

        [Description("For each signature and public key pair, OP_CHECKSIG is executed. If more public keys than signatures are listed, some key/sig pairs can fail. All signatures need to match a public key. If all signatures are valid, 1 is returned, 0 otherwise. Due to a bug, one extra unused value is removed from the stack.")]
        OP_CHECKMULTISIG = 0xAE,

        [Description("Same as OP_CHECKMULTISIG, but OP_VERIFY is executed afterward.")]
        OP_CHECKMULTISIGVERIFY = 0xAF,

        // Pseudo-words
        // These words are used internally for assisting with transaction matching. They are invalid if used in actual scripts.
        [Description("Represents a public key hashed with OP_HASH160.")]
        OP_PUBKEYHASH = 0xFD,

        [Description("Represents a public key compatible with OP_CHECKSIG.")]
        OP_PUBKEY = 0xFE,

        [Description("Matches any opcode that is not yet assigned.")]
        OP_INVALIDOPCODE = 0xFF,



        // Reserved words
        // Any opcode not assigned is also reserved. Using an unassigned opcode makes the transaction invalid.

        [Description("Transaction is invalid unless occuring in an unexecuted OP_IF branch")]
        OP_RESERVED = 0x50,

        [Description("Transaction is invalid unless occuring in an unexecuted OP_IF branch")]
        OP_VER = 0x62,

        [Description("Transaction is invalid even when occuring in an unexecuted OP_IF branch")]
        OP_VERIF = 0x65,

        [Description("Transaction is invalid even when occuring in an unexecuted OP_IF branch")]
        OP_VERNOTIF = 0x66,

        [Description("Transaction is invalid unless occuring in an unexecuted OP_IF branch")]
        OP_RESERVED1 = 0x89,

        [Description("Transaction is invalid unless occuring in an unexecuted OP_IF branch")]
        OP_RESERVED2 = 0x8A,

        [Description("The word is ignored.")]
        OP_NOP1 = 0xB0,

        [Description("The word is ignored.")]
        OP_NOP2 = 0xB1,

        [Description("The word is ignored.")]
        OP_NOP3 = 0xB2,

        [Description("The word is ignored.")]
        OP_NOP4 = 0xB3,

        [Description("The word is ignored.")]
        OP_NOP5 = 0xB4,

        [Description("The word is ignored.")]
        OP_NOP6 = 0xB5,

        [Description("The word is ignored.")]
        OP_NOP7 = 0xB6,

        [Description("The word is ignored.")]
        OP_NOP8 = 0xB7,

        [Description("The word is ignored.")]
        OP_NOP9 = 0xB8,

        [Description("The word is ignored.")]
        OP_NOP10 = 0xB9,
    }
}
