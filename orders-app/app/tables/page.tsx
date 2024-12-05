export default function Index() {
  return (
    <div className="flex-1 flex flex-col bg-white rounded-r-lg">
      <div className="p-5">
        <h1 className="text-xl font-bold">Wybierz stolik</h1>
        <div className="flex space-x-4 mt-4">
          <button className="px-4 py-2 bg-[#1E2A38] text-white rounded-md hover:bg-[#2B4B5C]">
            Bar
          </button>
          <button className="px-4 py-2 bg-[#1E2A38] text-white rounded-md hover:bg-[#2B4B5C]">
            Kominek
          </button>
          <button className="px-4 py-2 bg-[#1E2A38] text-white rounded-md hover:bg-[#2B4B5C]">
            Bilardownia
          </button>
          <button className="px-4 py-2 bg-[#1E2A38] text-white rounded-md hover:bg-[#2B4B5C]">
            Góra
          </button>
          <button className="px-4 py-2 bg-[#1E2A38] text-white rounded-md hover:bg-[#2B4B5C]">
            Ogródek
          </button>
        </div>
      </div>
      <div className="relative flex-1 bg-gray-100 rounded-lg">
        <div className="absolute top-10 left-10 flex flex-col items-center justify-center bg-blue-500 text-white rounded-lg p-4 shadow">
          <p className="text-lg font-bold">L1</p>
          <p>REZERWACJA</p>
          <p>14:30</p>
        </div>
        <div className="absolute top-10 right-10 flex flex-col items-center justify-center bg-[#1E2A38] text-white rounded-lg p-4 shadow">
          <p className="text-lg font-bold">P1</p>
          <p className="text-red-500">zł 400.00</p>
        </div>
        <div className="absolute bottom-10 left-10 flex flex-col items-center justify-center bg-gray-300 text-gray-800 rounded-lg p-4 shadow">
          <p className="text-lg font-bold">Stolik nr 1</p>
        </div>
        <div className="absolute bottom-10 right-10 flex flex-col items-center justify-center bg-gray-300 text-gray-800 rounded-lg p-4 shadow">
          <p className="text-lg font-bold">Stolik nr 3</p>
        </div>
      </div>
      <div className="flex justify-end space-x-4 p-5">
        <button className="px-6 py-2 bg-red-500 text-white rounded-md hover:bg-red-600">
          Usuń
        </button>
        <button className="px-6 py-2 bg-blue-500 text-white rounded-md hover:bg-blue-600">
          Dodaj
        </button>
      </div>
    </div>
  );
}
