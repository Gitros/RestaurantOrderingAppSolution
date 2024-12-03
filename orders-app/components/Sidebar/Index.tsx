import { BiFoodMenu } from "react-icons/bi";
import { IoMdCloseCircleOutline } from "react-icons/io";
import { LuSettings } from "react-icons/lu";
import { MdOutlineBorderColor, MdOutlineEditCalendar, MdOutlineTableBar } from "react-icons/md";

const Sidebar = () => (
    <nav className="w-1/7 p-5 flex flex-col h-full bg-gradient-to-b from-[#1E2A38] to-[#2B4B5C] text-white">
        <ul className="space-y-5 text-center flex flex-col flex-grow">
            <li className="flex flex-col items-center hover:text-blue-300 cursor-pointer">
                <MdOutlineTableBar className="w-2/5 h-3/5" />
                <span className="text-lg">Stoliki</span>
            </li>
            <li className="flex flex-col items-center hover:text-blue-300 cursor-pointer">
                <MdOutlineBorderColor className="w-2/5 h-3/5" />
                <span className="text-lg">Zamówienia</span>
            </li>
            <li className="flex flex-col items-center hover:text-blue-300 cursor-pointer">
                <BiFoodMenu className="w-2/5 h-3/5" />
                <span className="text-lg">Karta</span>
            </li>
            <li className="flex flex-col items-center hover:text-blue-300 cursor-pointer">
                <MdOutlineEditCalendar className="w-2/5 h-3/5" />
                <span className="text-lg">Rezerwacje</span>
            </li>
            <li className="flex flex-col items-center hover:text-blue-300 cursor-pointer">
                <IoMdCloseCircleOutline className="w-2/5 h-3/5" />
                <span className="text-lg">Koniec Dnia</span>
            </li>
        </ul>
        <ul className="text-center">
            <li className="flex flex-col items-center hover:text-blue-300 cursor-pointer">
                <LuSettings className="w-2/5 h-3/5" />
                <span className="text-lg">Ustawienia</span>
            </li>
        </ul>
    </nav>
);

export default Sidebar;
